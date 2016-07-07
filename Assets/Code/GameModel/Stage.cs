using System.Collections;

public class Stage {
	static int[] songBpm = {100, 120, 150};
	static int[] slowFactors = {-1,7,7,5,5,5,4,4,4,3,3,3,2,2,2,1,1,1}; //they totally won't complete the 1:s

	public int id;
	public int duration;
	public int slowFactor;
	public float musicParameter;
	public int ticksPerSecond; //maybe should be float instead
	public float secondsPerTick;

	public Stage(int id, float musicParameter) {
		this.id = id;
		this.slowFactor = slowFactors[id];

		this.musicParameter = musicParameter > 3.1f ? 0f : musicParameter > 2.6f ? 0.5f : musicParameter;
		int bpm = songBpm[id % 3];
		this.ticksPerSecond = (Constants.TICKS_PER_BEAT*bpm)/60; //will be slight rounding error...
		this.secondsPerTick = 1f/ticksPerSecond;
		this.duration = 10;
	}
}
