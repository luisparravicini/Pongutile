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
	
	public PBall () : base("Banana.png")
	{
		_scaleY = 0.25f;
		_scaleX = 0.1f;
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

}


