using System;
using UnityEngine;

public class PPlayer : FSprite
{
	private int _score;
	private float _speedY;
	
	public PPlayer () : base("player.png")
	{
		_score = 0;
	}

	public int score {
		get { return _score; }	
	}

	public void AddScore ()
	{
		_score++;
	}
	
	public float speedY {
		get { return _speedY; }
	}
	
	public void Move (PInputType move)
	{
		switch (move) {
		case PInputType.Down:
			_speedY = -7;
			break;
		case PInputType.Up:
			_speedY = 7;
			break;
		case PInputType.None:
			_speedY = 0;
			break;
		}
	}

	override public void Redraw (bool shouldForceDirty, bool shouldUpdateDepth)
	{
		float limitY = Futile.screen.halfHeight - height * anchorY;

		y += _speedY;
		if (Math.Abs (y) > limitY)
			y = limitY * (y < 0 ? -1 : 1);

		base.Redraw (shouldForceDirty, shouldUpdateDepth);
	}

}


