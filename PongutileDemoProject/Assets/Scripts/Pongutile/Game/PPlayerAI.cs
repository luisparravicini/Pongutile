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
	private float nextMove;
	private PInputType currentMove;
	private float _lastBallY;
	
	public PPlayerAI (PPlayer player, PBall ball)
	{
		_player = player;
		_ball = ball;
		_disabled = false;
		nextMove = 0;
		_lastBallY = 0;
	}

	public void Disable ()
	{
		_disabled = true;
	}
	
	public bool IsDisabled ()
	{
		return _disabled;
	}
	
	public void Update ()
	{
		if (_disabled)
			return;
	
		nextMove -= Time.deltaTime;
		if (nextMove > 0)
			return;

		nextMove = 0.1f;
		
		float distanceY = PUtil.UniDistance(_lastBallY, _ball.y);
		if (distanceY < 0.5f)
			currentMove = PInputType.None;
		else
			if (_player.y < _ball.y)
			currentMove = PInputType.Up;
		else if (_player.y > _ball.y)
			currentMove = PInputType.Down;

		_player.Move (currentMove);
		_lastBallY = _ball.y;
	}
}
