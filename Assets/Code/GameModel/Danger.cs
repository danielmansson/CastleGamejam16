using UnityEngine;
using System.Collections;

public class Danger {
	public enum Type
	{
		Block,
		Shot,
		Monster
	}
	public enum State
	{
		Alive,
		Dead
	}

	public Danger.Type type;
	public Danger.State state;
	public Player.Action requiredAction;
	public int hp;
	public int timestamp;

	public Danger(Danger.Type type, Player.Action requiredAction, int hp, int timestamp) {
		this.type = type;
		this.timestamp = timestamp;
		this.state = Danger.State.Alive;
		this.hp = hp;
		this.requiredAction = requiredAction;
	}
}
