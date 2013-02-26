using System;
using UnityEngine;


public enum PInputType
{
	None,
	Up,
	Down,
}

public class PInput
{
	private PInputType _player1Moves;
	private PInputType _player2Moves;
	
	public PInput ()
	{
	}

	public PInputType player1
	{
		get { return _player1Moves; }
	}
	
	public PInputType player2
	{
		get { return _player2Moves; }
	}

	public void Update()
	{
		_player1Moves = UserMoves(KeyCode.Q, KeyCode.A);
		_player2Moves = UserMoves(KeyCode.P, KeyCode.L);
	}

	public PInputType UserMoves(KeyCode up, KeyCode down)
	{
		PInputType result;
	
		if (Input.GetKey(up))
			result = PInputType.Up;
		else
		if (Input.GetKey(down))
			result = PInputType.Down;
		else
			result = PInputType.None;
		
		return result;
	}
}


