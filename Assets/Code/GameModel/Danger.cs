using UnityEngine;
using System.Collections;

public class Danger {
	public int id;
	public int timestamp;
	public int state;
	public int hp;
	public int requiredAction;

	public Danger(int id, int timestamp, int state, int hp, int requiredAction) {
		this.id = id;
		this.timestamp = timestamp;
		this.state = state;
		this.hp = hp;
		this.requiredAction = requiredAction;
	}
}
