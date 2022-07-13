using UnityEngine.Audio;
using System;
using UnityEngine;

namespace poopoostinky
{
[System.Serializable]
public class Sound
{
	public string name;

	public AudioClip clip;

	public float volume=1;
	public float pitch=1;

	public bool loop;

	[HideInInspector]
	public AudioSource source;
}


//cum
public class SFXMan : MonoBehaviour
{
	static Sound[] SND;
    static Sound[] MUS;

    public Sound[] sounds;
    public Sound[] musics;

    public static string cmus;

	void Awake() 
	{
		foreach (Sound s in sounds) 
		{
            s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.loop = s.loop;

			if (s.pitch<0) 
			{
                s.source.time = ((float)s.clip.length-.01f);
            }

            s.source.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("Ligma");
		}
        foreach (Sound s in musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = true;

            s.source.outputAudioMixerGroup = Resources.Load<AudioMixerGroup>("Ligma");
        }
        SND = sounds;
        MUS = musics;
    }

	public static void PlayMus(string name="")
    {
        foreach (Sound s in MUS)
        {
			if (s.source.loop)
				s.source.Stop ();
        }
		if (name != "" && name!="Stop")
			cmus = name;
		if (name != "Stop")
        {
            Sound s = Array.Find(MUS, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Music " + name + " Doesnt Exist");
                return;
            }
            s.source.Play();
        }
	}

	public static void Stop(string name)
    {
		Sound s = Array.Find (SND, sound => sound.name == name);
		if (s == null) 
		{
			Debug.LogWarning ("Sound " + name + " Doesnt Exist");
			return;
		}
		s.source.Stop();
	}

	public static void Play (string name)
	{
		Sound s = Array.Find (SND, sound => sound.name == name);
		if (s == null) 
		{
			Debug.LogWarning ("Sound " + name + " Doesnt Exist");
			return;
		}
        if (!s.source.isPlaying)
		s.source.Play();
	}
}
}
