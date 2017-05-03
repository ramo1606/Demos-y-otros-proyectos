using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour, IGameManager {
	public ManagerStatus status { get; private set; }

	public int health { get; private set; }
	public int maxHealth { get; private set; }

	private NetworkService _network;

	// Use this for initialization
	public void Startup (NetworkService service) 
	{
		Debug.Log ("Player Manager Starting...");

		UpdateData (50, 100);
		_network = service;

		status = ManagerStatus.Started;
	}

	public void UpdateData(int health, int maxHealth)
	{
		this.health = health;
		this.maxHealth = maxHealth;
	}

	public void ChangeHealth(int value)
	{
		health += value;

		if (health >= maxHealth) 
			health = maxHealth;
		if (health < 0)
			health = 0;

		if(health == 0)
		{
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}

		Messenger.Broadcast (GameEvent.HEALTH_UPDATED);
	}

	public void Respawn()
	{
		UpdateData (50, 100);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
