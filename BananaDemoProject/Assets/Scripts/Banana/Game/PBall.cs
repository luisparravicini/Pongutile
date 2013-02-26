using System;
using UnityEngine;

public enum PBorder
{
	None,
	Left,
	Right,
}


public class PBall : FSprite
{
	private Vector2 _velocity;
	private PBorder _borderTouched;
	
	public PBall () : base("ball.png")
	{
		Reset();		
	}
	
	public PBorder borderReached
	{
		get { return _borderTouched; }
	}
	
	public bool ReachedBorder()
	{
		return (_borderTouched != PBorder.None);
	}

	public void Reset ()
	{
		_velocity.x = 3.5f * PUtil.OneOrMinusOne();
		_velocity.y = 3.5f * PUtil.OneOrMinusOne();
		
		_borderTouched = PBorder.None;
	}
		
	override public void Redraw (bool shouldForceDirty, bool shouldUpdateDepth)
	{
		if (Math.Abs (x) > Futile.screen.halfWidth)
			_borderTouched = (x < 0 ? PBorder.Left : PBorder.Right);
		else {
			if (Math.Abs (y) > Futile.screen.halfHeight)
				_velocity.y *= -1;

			y += _velocity.y;
			x += _velocity.x;
		}
		
		base.Redraw (shouldForceDirty, shouldUpdateDepth);
	}

	public void CollidesWith (PPlayer player)
	{
		moveToPaddleBorder(player);
		
		_velocity.x *= -1;
		_velocity.y += player.speedY * 0.5f;
		_borderTouched = PBorder.None;
	}

	public void moveToPaddleBorder (PPlayer player)
	{
		x = player.x;
		float delta = player.width * player.anchorX + width*anchorX + 1;
		if (_borderTouched == PBorder.Left)
			x += delta;
		else
			x -= delta;

	}
}


