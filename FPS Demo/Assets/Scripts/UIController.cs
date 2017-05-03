using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	[SerializeField] private Text scoreLabel;			//reference to the score label
	[SerializeField] private SettingsPopUp popUpMenu;

	private int _score;

	void Awake()
	{
		Messenger.AddListener (GameEvent.ENEMY_HIT, OnEnemyHit);
	}


	void OnDestroy()
	{
		Messenger.RemoveListener (GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	// Use this for initialization
	void Start () 
	{
		_score = 0;
		scoreLabel.text = "Enemys Killed: " + _score;
		popUpMenu.Close ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//scoreLabel.text = Time.realtimeSinceStartup.ToString ();
	}

	public void OnOpenSettings()
	{
		popUpMenu.Open();
	}

	private void OnEnemyHit()
	{
		_score++;
		scoreLabel.text = "Enemys Killed: " + _score;
	}
}
