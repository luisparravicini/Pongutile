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
	private bool _startPlay;
	
	public PInput ()
	{
	}
	
	public bool startPlay
	{
		get { return _startPlay; }
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

		_startPlay = Input.GetKey(KeyCode.Space);
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


