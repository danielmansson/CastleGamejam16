using UnityEngine;
using System.Collections;

public class Player {
	public enum Action
	{
		Left,
		Right
	}
	public enum State
	{
		Idle,
		Intro,
		Safe,
		Outro
	}

	public int id;
	public int timestamp;
	public Player.State state;

	public Player(int id) {
		this.id = id;
		timestamp = 0;
		state = Player.State.Idle;
	}
}
