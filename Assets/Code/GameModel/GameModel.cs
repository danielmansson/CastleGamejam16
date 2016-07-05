using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameModel 
{
	public enum Action
	{
		Left,
		Right
	}

	public GameModel()
	{
		//Init with some nice data
	}

	public List<Timeline> GetTimelines()
	{
		return new List<Timeline>();
	}

	public void PerformAction(Action action)
	{
		Debug.Log("I wanna perform an action: " + action);
	}
}
