using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour       // Maybe implement as SIngelton?
{
	public static AudioManager audioManager;
	public Sound[] sounds;//TODO: Find Sounds

	void Awake()
	{
		if (audioManager != null)
		{
			Destroy(gameObject);
		}
		else
		{
			audioManager = this;
			DontDestroyOnLoad(gameObject);
		}
		//foreach (Sound s in sounds)
		//{
		//	//s.source = gameObject.AddComponent<AudioSource>();
		//	//s.source.clip = s.clip;
		//	//s.source.loop = s.loop;
		//  //s.source.playOnAwake = s.playOnAwake;
			
		//}
	}

	/// <summary>
	/// Add Sound and set Settings
	/// </summary>
	/// <param name="soundName"></param>
	/// <param name="sourceObject"></param>
	public void AddSound(string soundName, GameObject sourceObject)
	{
		Sound s = Array.Find(sounds, item => item.clip.name == soundName);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		sourceObject.AddComponent<AudioSource>();
		sourceObject.GetComponent<AudioSource>().clip = s.clip;
		sourceObject.GetComponent<AudioSource>().loop = s.loop;
		sourceObject.GetComponent<AudioSource>().volume = s.volume;
		sourceObject.GetComponent<AudioSource>().pitch = s.pitch;
		sourceObject.GetComponent<AudioSource>().playOnAwake = s.playOnAwake;
		sourceObject.GetComponent<AudioSource>().outputAudioMixerGroup = s.mixerGroup;
	}

	public void PlaySound(string soundType, GrabInteractable sourceObj)
	{
		if (GetComponent<AudioSource>() != null)
		{
			sourceObj.SoundSources = sourceObj.GetComponents<AudioSource>();
			foreach (AudioSource sound in sourceObj.SoundSources)
			{
				if (sound.clip.name == soundType)
				{
					sound.Play();
				}
				else
				{
					audioManager.AddSound(soundType, sourceObj.gameObject);
					sourceObj.SoundSources = sourceObj.GetComponents<AudioSource>();
					sourceObj.SoundSources.First(audios => audios.name == soundType).Play();
				}
			}
		}
		else
		{
			audioManager.AddSound(soundType, sourceObj.gameObject);
			sourceObj.SoundSources = sourceObj.GetComponents<AudioSource>();
			sourceObj.SoundSources[0].Play();
		}
	}

	//public void Play(string sound)
	//{
	//    Sound s = Array.Find(sounds, item => item.name == sound);
	//    if (s == null)
	//    {
	//        Debug.LogWarning("Sound: " + name + " not found!");
	//        return;
	//    }

	//    s.source.volume = s.volume;
	//    s.source.pitch = s.pitch;

	//    s.source.Play();
	//}
}
