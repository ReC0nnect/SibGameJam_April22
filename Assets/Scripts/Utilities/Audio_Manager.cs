using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class Audio_Manager : MonoBehaviour
{
	// Start is called before the first frame update
	public static Audio_Manager instance;

	public AudioMixerGroup SoundMixerGroup;

	public AudioMixerGroup MusicMixerGroup;

	public Sound[] sounds;

	public Sound[] musics;

	void Awake()
	{
		/*if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}*/

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = SoundMixerGroup;
		}

		foreach (Sound m in musics)
		{
			m.source = gameObject.AddComponent<AudioSource>();
			m.source.clip = m.clip;
			m.source.loop = m.loop;

			m.source.outputAudioMixerGroup = MusicMixerGroup;
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Play();
	}

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.Stop();
	}

	public void PlayMusic(string music)
	{



		Sound m = Array.Find(musics, item => item.name == music);
		if (m == null)
		{
			Debug.LogWarning("Music: " + name + " not found!");
			return;
		}

		m.source.volume = m.volume * (1f + UnityEngine.Random.Range(-m.volumeVariance / 2f, m.volumeVariance / 2f));
		m.source.pitch = m.pitch * (1f + UnityEngine.Random.Range(-m.pitchVariance / 2f, m.pitchVariance / 2f));

		if (PlayerPrefs.GetInt("OffMusic") == 1)
		{
			//	m.SetFloat("MusicVolume", -80f);
		}

		m.source.Play();
	}

	public void StopMusic(string music)
	{
		Sound m = Array.Find(musics, item => item.name == music);
		if (m == null)
		{
			Debug.LogWarning("Music: " + music + " not found!");
			return;
		}

		m.source.volume = m.volume * (1f + UnityEngine.Random.Range(-m.volumeVariance / 2f, m.volumeVariance / 2f));
		m.source.pitch = m.pitch * (1f + UnityEngine.Random.Range(-m.pitchVariance / 2f, m.pitchVariance / 2f));

		m.source.Stop();
	}
}
