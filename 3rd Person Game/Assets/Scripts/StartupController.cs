using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartupController : MonoBehaviour {
	[SerializeField] private Slider progressBar;

	// Use this for initialization
	void Awake () 
	{
		Messenger<int, int>.AddListener (StartUpEvent.MANAGERS_PROGRESS, OnManagersProgress);
		Messenger.AddListener (StartUpEvent.MANAGERS_STARTED, OnManagersStarted);
	}

	void OnDestroy()
	{
		Messenger<int, int>.RemoveListener (StartUpEvent.MANAGERS_PROGRESS, OnManagersProgress);
		Messenger.RemoveListener (StartUpEvent.MANAGERS_STARTED, OnManagersStarted);
	}

	private void OnManagersProgress(int numReady, int numModules)
	{
		float progress = (float)numReady / numModules;
		progressBar.value = progress;
	}

	private void OnManagersStarted()
	{
		Managers.Mission.GoToNext ();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
