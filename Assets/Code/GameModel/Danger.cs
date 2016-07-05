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

	public Danger(DangerData data) {
		this.type = data.type;
		this.timestamp = data.timestamp;
		this.hp = data.hp;
		this.requiredAction = data.requiredAction;
		this.state = Danger.State.Alive;
	}
}
