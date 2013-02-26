using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BInGamePage : BPage
{
	
	private FButton _closeButton;
	
	private FLabel _score2Label;
	private FLabel _score1Label;
	
	private int _frameCount = 0;
	
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

		BMain.instance.ResetScore();
		
		AddChild(_player1 = new PPlayer());
		AddChild(_player2 = new PPlayer());
		
		_player1.x = -Futile.screen.halfWidth * 0.8f;
		_player2.x = Futile.screen.halfWidth * 0.8f;
		
		
		_closeButton = new FButton("CloseButton_normal.png", "CloseButton_over.png", "ClickSound");
		AddChild(_closeButton);
		
		
		_closeButton.SignalRelease += HandleCloseButtonRelease;
		
		_score2Label = new FLabel("Franchise", BMain.instance.scorePlayer2.ToString());
		_score2Label.anchorX = 0.0f;
		_score2Label.anchorY = 1.0f;
		_score2Label.color = new Color(1.0f,0.90f,0.0f);
		
		_score1Label = new FLabel("Franchise", BMain.instance.scorePlayer1.ToString());
		_score1Label.anchorX = 1.0f;
		_score1Label.anchorY = 1.0f;
		_score1Label.color = new Color(1.0f,1.0f,1.0f);
		
		AddChild(_score2Label);
		AddChild(_score1Label);
		
		_effectHolder = new FContainer();
		AddChild (_effectHolder);
		
		_score2Label.alpha = 0.0f;
		Go.to(_score2Label, 0.5f, new TweenConfig().
			setDelay(0.0f).
			floatProp("alpha",1.0f));
		
		_score1Label.alpha = 0.0f;
		Go.to(_score1Label, 0.5f, new TweenConfig().
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
		
		_score2Label.x = -Futile.screen.halfWidth * 0.3f;
		_score2Label.y = Futile.screen.halfHeight - 10.0f;
		
		_score1Label.x = Futile.screen.halfWidth * 0.3f;
		_score1Label.y = Futile.screen.halfHeight - 10.0f;
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
				
		_score1Label.text = BMain.instance.scorePlayer1.ToString();
		_score2Label.text = BMain.instance.scorePlayer2.ToString();
		
		_frameCount++;
	}
}

