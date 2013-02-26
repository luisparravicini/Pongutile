using System;
using UnityEngine;

public class PUtil
{
	private PUtil ()
	{
	}
	
	public static int OneOrMinusOne()
	{
		return (RXRandom.Float(1) > 0.5 ? 1 : -1);
	}
	
	public static float UniDistance(float a, float b)
	{
		 return Mathf.Sqrt(Mathf.Pow(a - b, 2));
	}

}


