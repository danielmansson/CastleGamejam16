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
}
