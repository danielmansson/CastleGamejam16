using System.Collections;

[System.Serializable]

public class DangerData {
	public Danger.Type type;
	public Player.Action requiredAction;
	public int hp;
	public int timestamp;

	public DangerData(Danger.Type type, Player.Action requiredAction, int hp, int timestamp) {
		this.type = type;
		this.timestamp = timestamp;
		this.hp = hp;
		this.requiredAction = requiredAction;
	}

	public DangerData()
	{
	}
}
