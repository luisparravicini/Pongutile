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
	
	public PPlayerAI (PPlayer player, PBall ball)
	{
		_player = player;
		_ball = ball;
		_disabled = false;
		nextMove = 0;
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

		float delta = _player.height * 0.7f;
		
		if (Math.Abs (Math.Abs (_player.y) - Math.Abs (_ball.y)) < delta)
			currentMove = PInputType.None;
		else if (_player.y < _ball.y)
			currentMove = PInputType.Up;
		else if (_player.y > _ball.y)
			currentMove = PInputType.Down;
		else
			currentMove = PInputType.None;

		_player.Move (currentMove);
	}
}
