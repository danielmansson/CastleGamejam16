using UnityEngine;
using System.Collections;

[System.Serializable]

public class DangerData {
	public Danger.Type type;
	public Player.Action requiredAction;
	public int hp;
	public int timestamp;
}
