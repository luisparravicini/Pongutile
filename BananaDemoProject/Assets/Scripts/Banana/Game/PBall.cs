using System;
using UnityEngine;

public class PBall : FSprite
{
	private Vector2 _velocity;
	
	public PBall () : base("Banana.png")
	{
		_scaleY = 0.25f;
		_scaleX = 0.1f;
		
		_velocity.x = 3.5f;
		_velocity.y = 3.5f;
	}
	
	override public void Redraw(bool shouldForceDirty, bool shouldUpdateDepth)
	{
		if (Math.Abs(x) > Futile.screen.halfWidth)
			_velocity.x *= -1;
		else
			if (Math.Abs(y) > Futile.screen.halfHeight)
				_velocity.y *= -1;

		y += _velocity.y;
		x += _velocity.x;
		
		base.Redraw(shouldForceDirty, shouldUpdateDepth);
	}

}


