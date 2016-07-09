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
	public int difficulty;
	public int timestamp;
	public int distanceLeft;

	public event System.Action OnDestroy;
	public event System.Action OnHit;

	public Danger(DangerData data) {
		this.type = data.type;
		this.timestamp = data.timestamp;
		this.hp = data.hp;
		this.requiredAction = data.requiredAction;
		this.state = Danger.State.Alive;

		distanceLeft = timestamp;
	}

	public Danger(Danger.Type type, Player.Action requiredAction, int hp, int timestamp, int difficulty) {
		this.type = type;
		this.timestamp = timestamp;
		this.hp = hp;
		this.requiredAction = requiredAction;
		this.state = Danger.State.Alive;
		this.difficulty = difficulty;

		distanceLeft = timestamp;
	}

	public void Step(int currentFrame)
	{
		distanceLeft--;
		this.state = Danger.State.Alive;
	}

	public void Hit()
	{
		hp--;
		if (OnHit != null)
		{
			OnHit();
		}
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
