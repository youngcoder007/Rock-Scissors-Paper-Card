using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	[Serializable]
	public class Sound
	{
		public string name;
		public AudioClip clip;
		[Range(0f, 1f)]
		public float volume;
		public bool loop;
		[HideInInspector]
		public AudioSource source;
	}

	public static AudioManager Instance;
	public Sound[] sounds;

	private void Awake()
	{
		Instance = this;

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.volume = s.volume;
			s.source.playOnAwake = false;
			s.source.loop = s.loop;
		}
	}

	public void Play(string name)
	{
		Sound s = Array.Find(sounds, x => x.name == name);
		if (s == null)
		{
			Debug.LogWarning("Can't find sound: " + name);
			return;
		}
		s.source.Play();
	}
}
