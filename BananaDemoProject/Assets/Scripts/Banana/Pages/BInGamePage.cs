using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BInGamePage : BPage
{
	
	private FButton _closeButton;
	
	private FLabel _scoreLabel;
	private FLabel _timeLabel;
	
	private int _frameCount = 0;
	private float _secondsLeft = 15.9f;
	
	private FContainer _effectHolder;
	
	private PPlayer _player1;
	private PPlayer _player2;
	
	private PInput _userInput;

	public BInGamePage()
	{
		
	}
	
	override public void HandleAddedToStage()
	{
		Futile.instance.SignalUpdate += HandleUpdate;
		Futile.screen.SignalResize += HandleResize;
		base.HandleAddedToStage();	
	}
	
	override public void HandleRemovedFromStage()
	{
		Futile.instance.SignalUpdate -= HandleUpdate;
		Futile.screen.SignalResize -= HandleResize;
		base.HandleRemovedFromStage();	
	}
	
	override public void Start()
	{
		_userInput = new PInput();

		BMain.instance.score = 0;
		
		AddChild(_player1 = new PPlayer());
		AddChild(_player2 = new PPlayer());
		
		_player1.x = -Futile.screen.halfWidth * 0.8f;
		_player2.x = Futile.screen.halfWidth * 0.8f;
		
		
		_closeButton = new FButton("CloseButton_normal.png", "CloseButton_over.png", "ClickSound");
		AddChild(_closeButton);
		
		
		_closeButton.SignalRelease += HandleCloseButtonRelease;
		
		_scoreLabel = new FLabel("Franchise", "0 Bananas");
		_scoreLabel.anchorX = 0.0f;
		_scoreLabel.anchorY = 1.0f;
		_scoreLabel.scale = 0.75f;
		_scoreLabel.color = new Color(1.0f,0.90f,0.0f);
		
		_timeLabel = new FLabel("Franchise", ((int)_secondsLeft) + " Seconds Left");
		_timeLabel.anchorX = 1.0f;
		_timeLabel.anchorY = 1.0f;
		_timeLabel.scale = 0.75f;
		_timeLabel.color = new Color(1.0f,1.0f,1.0f);
		
		AddChild(_scoreLabel);
		AddChild(_timeLabel);
		
		_effectHolder = new FContainer();
		AddChild (_effectHolder);
		
		_scoreLabel.alpha = 0.0f;
		Go.to(_scoreLabel, 0.5f, new TweenConfig().
			setDelay(0.0f).
			floatProp("alpha",1.0f));
		
		_timeLabel.alpha = 0.0f;
		Go.to(_timeLabel, 0.5f, new TweenConfig().
			setDelay(0.0f).
			floatProp("alpha",1.0f).
			setEaseType(EaseType.BackOut));
		
		_closeButton.scale = 0.0f;
		Go.to(_closeButton, 0.5f, new TweenConfig().
			setDelay(0.0f).
			floatProp("scale",1.0f).
			setEaseType(EaseType.BackOut));
		
		HandleResize(true); //force resize to position everything at the start
	}
	
	protected void HandleResize(bool wasOrientationChange)
	{
		//this will scale the background up to fit the screen
		//but it won't let it shrink smaller than 100%
		 
		_closeButton.x = -Futile.screen.halfWidth + 30.0f;
		_closeButton.y = -Futile.screen.halfHeight + 30.0f;
		
		_scoreLabel.x = -Futile.screen.halfWidth + 10.0f;
		_scoreLabel.y = Futile.screen.halfHeight - 10.0f;
		
		_timeLabel.x = Futile.screen.halfWidth - 10.0f;
		_timeLabel.y = Futile.screen.halfHeight - 10.0f;
	}

	private void HandleCloseButtonRelease (FButton button)
	{
		BMain.instance.GoToPage(BPageType.TitlePage);
	}

	protected void HandleUpdate ()
	{
		_userInput.Update();
		
		_player1.Move(_userInput.player1);
		_player2.Move(_userInput.player2);
		
		_secondsLeft -= Time.deltaTime;
		
		if(_secondsLeft <= 0)
		{
			BSoundPlayer.PlayVictoryMusic();
			BMain.instance.GoToPage(BPageType.ScorePage);
			return;
		}
		
		_timeLabel.text = ((int)_secondsLeft) + " Seconds Left";
		
		if(_secondsLeft < 10) //make the timer red with 10 seconds left
		{
			_timeLabel.color = new Color(1.0f,0.2f,0.0f);
		}
		
		_frameCount++;
	}
}

