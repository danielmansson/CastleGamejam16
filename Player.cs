using UnityEngine;
using System.Collections;

public class Player {
	public int id;
	public int timestamp;
	public int state;

	public Player(int id) {
		this.id = id;
		timestamp = 0;
		state = 0;
	}
}
