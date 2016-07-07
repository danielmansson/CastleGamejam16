using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventChangeGameState : EventArgs
{
	public State From;
	public State To;

	public EventChangeGameState(State from, State to)
	{
		From = from;
		To = to;
	}
}

public class EventLevelLoad : EventArgs
{
	public string LevelName;

	public EventLevelLoad(string name)
	{
		LevelName = name;
	}
}

public class EventPlayAudio : EventArgs
{
    public string Name;

    public EventPlayAudio(string name)
    {
        Name = name;
    }
}


public class EventChangeParameter : EventArgs
{
    public string SoundName;
    public string Parameter;
    public float Value;

    public EventChangeParameter(string soundName, string parameter, float value)
    {
        SoundName = soundName;
        Parameter = parameter;
        Value = value;
    }
}

class AudioEvent
{
    public static void Play(string name)
    {
        EventManager.Instance.SendEvent<EventPlayAudio>(new EventPlayAudio(name));
    }

    public static void ChangeParameter(string soundName, string parameter, float value)
    {
        EventManager.Instance.SendEvent<EventChangeParameter>(new EventChangeParameter(soundName, parameter, value));
    }
}

