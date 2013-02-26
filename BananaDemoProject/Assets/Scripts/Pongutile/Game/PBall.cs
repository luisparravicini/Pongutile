using System;
using UnityEngine;

public enum PSide
{
	None,
	Left,
	Right,
}

public class PBall : FSprite
{
	private Vector2 _velocity;
	private PSide _borderTouched;
	public bool canGoOutOfBounds;
	private const float SPEED = 3.5f;
	
	public PBall () : base("ball.png")
	{
		canGoOutOfBounds = true;
		Reset ();
	}
	
	public PSide borderReached {
		get { return _borderTouched; }
	}
	
	public bool ReachedBorder ()
	{
		return (_borderTouched != PSide.None);
	}

	public void Reset ()
	{
		_velocity.x = SPEED * PUtil.OneOrMinusOne ();
		_velocity.y = SPEED * PUtil.OneOrMinusOne ();
		
		_borderTouched = PSide.None;
	}
	
	override public void Redraw (bool shouldForceDirty, bool shouldUpdateDepth)
	{
		if (canGoOutOfBounds && Math.Abs (x) > Futile.screen.halfWidth)
			_borderTouched = (x < 0 ? PSide.Left : PSide.Right);
		else {
			if (Math.Abs (x) > Futile.screen.halfWidth)
				_velocity.x *= -1;
			if (Math.Abs (y) > Futile.screen.halfHeight)
				_velocity.y *= -1;

			y += _velocity.y;
			x += _velocity.x;
		}
		
		base.Redraw (shouldForceDirty, shouldUpdateDepth);
	}

	public void CollidesWith (PPlayer player, PSide side)
	{
		moveToPaddleBorder (player, side);
		
		_velocity.x *= -1;
		
		// Sometimes, touching the center of the paddle doubles the ball Y speed.
		// This is to "fix" the case when both players are controlled by the AI, the ball has speedY == 0
		// and none of the AI moves.
		if (RXRandom.Float (1) > 0.3f && Math.Abs (player.speedY) < 0.1)
			_velocity.y = SPEED * PUtil.OneOrMinusOne ();
		else
			_velocity.y += player.speedY * 0.5f;
	}

	public void moveToPaddleBorder (PPlayer player, PSide side)
	{
		x = player.x;
		float delta = player.width * player.anchorX + width * anchorX + 1;
		if (side == PSide.Left)
			x += delta;
		else
			x -= delta;

	}
}


