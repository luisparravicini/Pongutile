using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BInGamePage : BPage
{	
	private PBall _ball;
	private PInput _userInput;
	private FLabel _score2Label;
	private FLabel _score1Label;
	private PPlayer _player1;
	private PPlayer _player2;
	private FLabel _player1Hint;
	private FLabel _player2Hint;
	private PPlayerAI _player1AI;
	private PPlayerAI _player2AI;

	public BInGamePage ()
	{
		
	}
	
	override public void HandleAddedToStage ()
	{
		Futile.instance.SignalUpdate += HandleUpdate;
		Futile.screen.SignalResize += HandleResize;
		base.HandleAddedToStage ();	
	}
	
	override public void HandleRemovedFromStage ()
	{
		Futile.instance.SignalUpdate -= HandleUpdate;
		Futile.screen.SignalResize -= HandleResize;
		base.HandleRemovedFromStage ();	
	}
	
	override public void Start ()
	{
		_userInput = new PInput ();

		BMain.instance.ResetScore ();
		
		AddChild (_player1 = new PPlayer ());
		AddChild (_player2 = new PPlayer ());
		
		_player1.x = -Futile.screen.halfWidth * 0.8f;
		_player2.x = Futile.screen.halfWidth * 0.8f;
		
		AddChild (_ball = new PBall ());
		ThrowNewBall ();
		
		_player1AI = new PPlayerAI(_player1, _ball);
		_player2AI = new PPlayerAI(_player2, _ball);
		
		BInGamePage.AddLineMiddle (this);
		
		_score2Label = new FLabel ("Franchise", BMain.instance.scorePlayer2.ToString ());
		_score2Label.anchorX = 0.0f;
		_score2Label.anchorY = 1.0f;

		_score1Label = new FLabel ("Franchise", BMain.instance.scorePlayer1.ToString ());
		_score1Label.anchorX = 1.0f;
		_score1Label.anchorY = 1.0f;
		
		AddChild (_score2Label);
		AddChild (_score1Label);
		
		// TODO player 1 keys are defined in PInput. The code should ask PInput and not hardcode it here
		AddChild (_player1Hint = new FLabel ("Franchise", "Q and A to play"));
		_player1Hint.scale = 0.4f;

		// TODO player 2 keys are defined in PInput. The code should ask PInput and not hardcode it here
		AddChild (_player2Hint = new FLabel ("Franchise", "P and L to play"));
		_player2Hint.scale = 0.4f;

		_player1Hint.alpha = 0.15f;
		//TODO should remove it instead of setting alpha = 0
		Go.to (_player1Hint, 0.9f, new TweenConfig ().
			floatProp ("alpha", 1.0f).setIterations (10, LoopType.PingPong).floatProp("alpha", 0));
		
		_player2Hint.alpha = 0.15f;
		//TODO should remove it instead of setting alpha = 0
		Go.to (_player2Hint, 0.9f, new TweenConfig ().
			floatProp ("alpha", 1.0f).setIterations (10, LoopType.PingPong).floatProp("alpha", 0));

		HandleResize (true); //force resize to position everything at the start
	}

	public static void AddLineMiddle (FContainer container)
	{
		FSprite middle = new FSprite ("middle.png");
		float y = Futile.screen.halfHeight - middle.height;
		while (y > -Futile.screen.halfHeight + middle.height) {
			middle = new FSprite ("middle.png");
			middle.x = 0;
			middle.y = y;
			container.AddChild (middle);
			
			y -= middle.height * 1.5f;
		}
	}
	
	protected void ThrowNewBall ()
	{
		_ball.Reset ();
		_ball.x = _ball.width * 1.5f * PUtil.OneOrMinusOne ();
		_ball.y = RXRandom.Range (0, Futile.screen.halfHeight - _ball.height) * PUtil.OneOrMinusOne ();
	}
	
	protected void HandleResize (bool wasOrientationChange)
	{
		_score2Label.x = -Futile.screen.halfWidth * 0.3f;
		_score2Label.y = Futile.screen.halfHeight - 10.0f;
		
		_score1Label.x = Futile.screen.halfWidth * 0.3f;
		_score1Label.y = Futile.screen.halfHeight - 10.0f;
		
		_player1Hint.x = -Futile.screen.halfWidth * 0.5f;
		_player1Hint.y = 0;

		_player2Hint.x = Futile.screen.halfWidth * 0.5f;
		_player2Hint.y = 0;
	}

	protected void HandleUpdate ()
	{
		UpdateInput ();
		CheckCollisions ();
		CheckBallOutOfBounds ();		
		UpdateScores ();
	}

	protected void UpdateInput ()
	{
		_userInput.Update ();
		
		if (_userInput.player1 != PInputType.None)
			_player1AI.Disable();
		
		if (_userInput.player2 != PInputType.None)
			_player2AI.Disable();
		
		_player1.Move (_userInput.player1);
		_player2.Move (_userInput.player2);
		_player1AI.Update();
		_player2AI.Update();
		
		if (_userInput.goBack)
			BMain.instance.GoToPage (BPageType.TitlePage);
	}

	protected void CheckCollisions ()
	{
		Rect ballRect = _ball.textureRect.CloneAndOffset (_ball.x, _ball.y);
		Rect playerRect = _player1.textureRect.CloneAndOffset (_player1.x, _player1.y);

		if (playerRect.CheckIntersect (ballRect))
			_ball.CollidesWith (_player1);
		else {
			playerRect = _player2.textureRect.CloneAndOffset (_player2.x, _player2.y);
			if (playerRect.CheckIntersect (ballRect))
				_ball.CollidesWith (_player2);
		}
	}

	protected void CheckBallOutOfBounds ()
	{
		if (_ball.ReachedBorder ()) {
			if (_ball.borderReached == PBorder.Left)
				BMain.instance.scorePlayer2++;
			else
				BMain.instance.scorePlayer1++;
			
			ThrowNewBall ();
		}
	}

	protected void UpdateScores ()
	{
		_score1Label.text = BMain.instance.scorePlayer1.ToString ();
		_score2Label.text = BMain.instance.scorePlayer2.ToString ();
	}
}

