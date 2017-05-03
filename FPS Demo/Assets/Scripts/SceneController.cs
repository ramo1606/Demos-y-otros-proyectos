using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {

	[SerializeField] private GameObject enemyPrefab;			//reference to enemy prefab
	private GameObject _enemy;									//reference to enemy instance

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Just create a new enemy if there is no enemy in the scene
		if (_enemy == null) 
		{
			//Instantiate a new enemy
			_enemy = Instantiate (enemyPrefab) as GameObject;
			//Set the enemy position
			_enemy.transform.position = new Vector3 (0, 1, 0);

			//Set the movement angle of the enemy to a random angle
			float angle = Random.Range (0, 360);
			_enemy.transform.Rotate (0, angle, 0);

			_enemy.GetComponent<WanderingAI>().OnSpeedChanged (PlayerPrefs.GetFloat ("Speed", 1));
		}
	}
}
