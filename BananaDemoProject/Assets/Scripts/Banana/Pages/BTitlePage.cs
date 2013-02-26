using UnityEngine;
using System.Collections;
using System;

public class BTitlePage : BPage
{
	private int _frameCount = 0;
	private PInput _input;
	private FLabel _title;
	private FLabel _subtitle;
	private FLabel _playLabel;
	private PBall _ball;
	
	
	public BTitlePage()
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
		_input = new PInput();
		
		AddChild(_title = new FLabel("Franchise", "Pongutile"));
		_title.scale = 1.5f;
		_title.anchorY = 1;
		
		AddChild(_subtitle = new FLabel("Franchise", "pong + futile"));
		_subtitle.scale = 0.7f;
		_subtitle.anchorY = 1;
		
		AddChild(_playLabel = new FLabel("Franchise", "space to play"));
		_playLabel.scale = 0.5f;
		
		BInGamePage.AdLineMiddle(this);
		
		AddChild (_ball = new PBall ());
		_ball.canGoOutOfBounds = false;

		HandleResize(true); //force resize to position everything at the start
	}
	
	protected void HandleResize(bool wasOrientationChange)
	{
		_title.x = 0;
		_title.y = Futile.screen.halfHeight * 0.8f;

		_subtitle.x = 0;
		_subtitle.y = _title.y - _title.textRect.CloneAndMultiply(_title.scale).CloneAndOffset(_title.x, _title.y).height;
		
		_playLabel.x = 0;
		_playLabel.y = - Futile.screen.halfHeight * 0.5f;
	}

	protected void HandleUpdate ()
	{
		_frameCount++;
		_input.Update();
		
		if (_input.startPlay)
			BMain.instance.GoToPage(BPageType.InGamePage);
	}

}

