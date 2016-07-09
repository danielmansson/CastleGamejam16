using System.Collections;
using UnityEngine;

public class Stage {
	static int[] songBpm = {100, 120, 150};

	public int id;
	public int duration;
	public int slowFactor;
	public float musicParameter;
	public int ticksPerSecond; //maybe should be float instead
	public float secondsPerTick;

	public Stage(int id) {
		this.id = id;
		this.slowFactor = Mathf.Max(1, 9 - id/3); //8->1
		if(id < 3) this.slowFactor--; //tired uglyfix
		this.musicParameter = (id % 3) + ((id % 6)/3)/2f; //i=0 -> i= 8 => 0, 1, 2, 0.5, 1.5, 2.5, 0, 1, 2...

		int bpm = songBpm[id % 3];
		this.ticksPerSecond = (Constants.TICKS_PER_BEAT*bpm)/60; //will be slight rounding error...
		this.secondsPerTick = 1f/ticksPerSecond;
		this.duration = 13;
	}
}
