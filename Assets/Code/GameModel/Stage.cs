using System.Collections;

public class Stage {
	static string[] stageSongs = {"Music100", "Music120", "Music150"};
	static int[] songBpm = {100, 120, 150};
	static int[] slowFactors = {-1,7,7,5,5,5,4,4,4,3,3,3,2,2,2,1,1,1}; //they totally won't complete the 1:s

	public int id;
	public int duration;
	public int slowFactor;
	public string song;
	public int ticksPerSecond; //maybe should be float instead
	public float secondsPerTick;

	public Stage(int id) {
		this.id = id;
		this.duration = 10;
		this.song = stageSongs[id % 3];
		this.slowFactor = slowFactors[id];

		int bpm = songBpm[id % 3];
		this.ticksPerSecond = (Constants.TICKS_PER_BEAT*bpm)/60; //will be slight rounding error...
		this.secondsPerTick = 1f/ticksPerSecond;
	}
}
