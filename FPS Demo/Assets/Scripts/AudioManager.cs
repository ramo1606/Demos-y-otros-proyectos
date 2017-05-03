﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour, IGameManager {

	[SerializeField] private AudioSource soundSource;
	[SerializeField] private AudioSource music1Source;
	[SerializeField] private AudioSource music2Source;

	[SerializeField] private string introBGMusic;
	[SerializeField] private string levelBGMusic;

	public ManagerStatus status { get; private set; }
	public float soundVolume
	{
		get { return AudioListener.volume; }
		set { AudioListener.volume = value; }
	}

	public bool soundMute
	{
		get { return AudioListener.pause; }
		set { AudioListener.pause = value; }
	}

	private float _musicVolume;

	public float musicVolume
	{
		get { return _musicVolume; }
		set 
		{	_musicVolume = value; 
		
			if (music1Source != null && !_crossFading) 
			{
				music1Source.volume = _musicVolume;
				music2Source.volume = _musicVolume;
			}
		}
	}

	public bool musicMute
	{
		get { 
			if (music1Source != null) 
			{
				return music1Source.mute;
			}
			return false; 
		}
		set { 
			if (music1Source != null) {
				music1Source.mute = value;
				music2Source.mute = value; 
			}
		}
	}

	private NetworkService _network;
	private AudioSource _activeMusic;
	private AudioSource _inactiveMusic;

	public float crossFadeRate = 1.5f;
	private bool _crossFading;

	// Use this for initialization
	public void Startup (NetworkService service) 
	{
		Debug.Log ("Audio Manager Starting...");

		_network = service;

		music1Source.ignoreListenerVolume = true;
		music1Source.ignoreListenerPause = true;
		music2Source.ignoreListenerVolume = true;
		music2Source.ignoreListenerPause = true;

		soundVolume = 1f;
		_musicVolume = 1f;

		_activeMusic = music1Source;
		_inactiveMusic = music2Source;

		status = ManagerStatus.Started;
	}

	public void PlaySound(AudioClip clip)
	{
		soundSource.PlayOneShot (clip);
	}

	public void PlayIntroMusic()
	{
		PlayMusic (Resources.Load("Music/"+introBGMusic) as AudioClip);
	}

	public void PlayLevelMusic()
	{
		PlayMusic (Resources.Load("Music/"+levelBGMusic) as AudioClip);
	}

	private void PlayMusic(AudioClip clip)
	{
		if (_crossFading) {return;}
		StartCoroutine(CrossFadeMusic(clip));
	}

	public void StopMusic()
	{
		music1Source.Stop ();
	}


	private IEnumerator CrossFadeMusic(AudioClip clip)
	{
		_crossFading = true;

		_inactiveMusic.clip = clip;
		_inactiveMusic.volume = 0;
		_inactiveMusic.Play ();

		float scaleRate = crossFadeRate - _musicVolume;
		while(_activeMusic.volume > 0)
		{
			_activeMusic.volume -= scaleRate * Time.deltaTime;
			_inactiveMusic.volume += scaleRate * Time.deltaTime;

			yield return null;
		}

		AudioSource temp = _activeMusic;

		_activeMusic = _inactiveMusic;
		_activeMusic.volume = _musicVolume;

		_inactiveMusic = temp;
		_inactiveMusic.Stop ();

		_crossFading = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
