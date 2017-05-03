using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsPopUp : MonoBehaviour {

	[SerializeField] private Slider speedSlider;		//reference to the speed slider
	[SerializeField] private AudioClip sound;

	//Show the menu
	public void Open()
	{
		gameObject.SetActive (true);
	}

	//Hide the menu
	public void Close()
	{
		//set the new value in the slider
		PlayerPrefs.SetFloat ("Speed", speedSlider.value);
		//save the changes
		PlayerPrefs.Save ();
		gameObject.SetActive (false);
	}

	//Set the name that was changed in the input field
	public void OnSubmitName(string name)
	{
		Debug.Log (name);
	}

	//Broadcast the speed value that was changed with the slider 
	public void OnSpeedValue(float speed)
	{
		Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
	}

	public void OnSoundToggle()
	{
		Managers.Audio.PlaySound (sound);
		Managers.Audio.soundMute = !Managers.Audio.soundMute;
	}

	public void OnSoundValue(float volume)
	{
		Managers.Audio.soundVolume = volume;
	}

	public void OnPlayMusic(int selector)
	{
		Managers.Audio.PlaySound (sound);

		switch (selector) 
		{
		case 1:
			Managers.Audio.PlayIntroMusic ();
			break;
		case 2:
			Managers.Audio.PlayLevelMusic ();
			break;
		default:
			Managers.Audio.StopMusic ();
			break;
		}
	}

	public void OnMusicToggle()
	{
		Managers.Audio.musicMute = !Managers.Audio.musicMute;
		Managers.Audio.PlaySound (sound);
	}

	public void OnMusicValue(float volume)
	{
		Managers.Audio.musicVolume = volume;
	}

	// Use this for initialization
	void Start () 
	{
		//Set the slider with the value saved in PlayerPrefs
		speedSlider.value = PlayerPrefs.GetFloat ("Speed", 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
