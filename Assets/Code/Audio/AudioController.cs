using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public string name;
        public string fmodName;
    }

    [SerializeField]
    List<Entry> m_entries;

    class SoundData
    {
        public string name;
        public string fmodName;
        [FMODUnity.EventRef]
        public FMOD.Studio.EventInstance instance;
    }

    Dictionary<string, SoundData> m_sounds = new Dictionary<string, SoundData>();

	void Start ()
    {
        EventManager.Instance.RegisterEvent<EventPlayAudio>(HandleAudioEvent);
        EventManager.Instance.RegisterEvent<EventChangeParameter>(HandleParameterChange);

        SetupSounds();
	}

    void SetupSounds()
    {
        foreach (var entry in m_entries)
        {
            var soundData = new SoundData()
            {
                name = entry.name,
                fmodName = entry.fmodName,
                instance = FMODUnity.RuntimeManager.CreateInstance (entry.fmodName)
            };

            m_sounds.Add(entry.name, soundData);
        }
    }

    void HandleParameterChange(EventChangeParameter args)
    {
        SoundData soundData;
        if (m_sounds.TryGetValue(args.SoundName, out soundData))
        {
            ChangeParameter(soundData, args.Parameter, args.Value);
        }
        else
        {
            Debug.LogWarning("Parameter change, sound missing: " + args.SoundName);
        }
    }

    void HandleAudioEvent(EventPlayAudio args)
    {
       // Debug.Log("I wanna play some sound named: " + args.Name);

        SoundData soundData;
        if (m_sounds.TryGetValue(args.Name, out soundData))
        {
            PlaySound(soundData);
        }
        else
        {
            Debug.LogWarning("Sound missing: " + args.Name);
        }
    }

    void PlaySound(SoundData soundData)
    {
        soundData.instance.start();
    }

    void ChangeParameter(SoundData soundData, string parameterName, float value)
    {
        
        FMOD.Studio.ParameterInstance parameter;
        if (soundData.instance.getParameter(parameterName, out parameter) == FMOD.RESULT.OK)
        {
            parameter.setValue(value);
        }
        else
        {
            Debug.LogWarning("Can't get parameter: " + soundData.fmodName + ", " + parameterName);
        }
        
    }
}
