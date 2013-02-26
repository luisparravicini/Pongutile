using System;
using UnityEngine;

public class PPlayer : FSprite
{
	private int _score;
	private float _speedY;
	private float _minY;
	private float _maxY;
	
	public PPlayer () : base("player.png")
	{
		_score = 0;
		
		_maxY = Futile.screen.halfWidth - height * anchorY;
		_minY = -Futile.screen.halfWidth + height * anchorY;	
	}

	public int score
	{
		get {return _score;}	
	}

	public void AddScore()
	{
		_score++;
	}	
	
	public float speedY
	{
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

	override public void Redraw(bool shouldForceDirty, bool shouldUpdateDepth)
	{
		if (y > _maxY)
			y = _maxY;
		else
		if (y < _minY)
			y = _minY;
		else
			y += _speedY;

		base.Redraw(shouldForceDirty, shouldUpdateDepth);
	}

}


