using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Very simple AI for a player. Just follows the ball.
/// </summary>
public class PPlayerAI
{
	private PPlayer _player;
	private PBall _ball;
	private bool _disabled;
	
	public PPlayerAI (PPlayer player, PBall ball)
	{
		_player = player;
		_ball = ball;
		_disabled = false;
	}

	public void Disable ()
	{
		_disabled = true;
	}
	
	public void Update ()
	{
		if (_disabled)
			return;
	
		float delta = _player.height * 0.2f;
		
		if (Math.Abs(Math.Abs(_player.y) - Math.Abs(_ball.y)) < delta)
			_player.Move(PInputType.None);
		else
		if (_player.y < _ball.y)
			_player.Move (PInputType.Up);
		else if (_player.y > _ball.y)
			_player.Move (PInputType.Down);
		else
			_player.Move (PInputType.None);
				
	}
}
