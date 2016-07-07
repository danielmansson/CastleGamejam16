using UnityEngine;
using System.Collections;

public class Danger : System.IComparable<Danger>
{
	public enum Type
	{
		Shot,
		Block,
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
	public int distanceLeft;

	public event System.Action OnDestroy;

	public Danger(DangerData data) {
		this.type = data.type;
		this.timestamp = data.timestamp;
		this.hp = data.hp;
		this.requiredAction = data.requiredAction;
		this.state = Danger.State.Alive;

		distanceLeft = timestamp;
	}

	public Danger(Danger.Type type, Player.Action requiredAction, int hp, int timestamp) {
		this.type = type;
		this.timestamp = timestamp;
		this.hp = hp;
		this.requiredAction = requiredAction;
		this.state = Danger.State.Alive;

		distanceLeft = timestamp;
	}

	public void Step(int currentFrame)
	{
		distanceLeft--;
		this.state = Danger.State.Alive;
	}

	public void Destroy()
	{
		if (OnDestroy != null)
		{
			OnDestroy();
		}
	}

	public int CompareTo(Danger other)
	{
		return distanceLeft - other.distanceLeft;
	}
}
