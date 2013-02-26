using System;
using UnityEngine;

public class PPlayer : FSprite
{
	private int _score;
	private float _speedY;
	
	public PPlayer () : base("Banana.png")
	{
		_score = 0;
	}
	
	public int score
	{
		get {return _score;}	
	}
	
	public void AddScore()
	{
		_score++;
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
		this.y += _speedY;
		
		base.Redraw(shouldForceDirty, shouldUpdateDepth);
	}

}


