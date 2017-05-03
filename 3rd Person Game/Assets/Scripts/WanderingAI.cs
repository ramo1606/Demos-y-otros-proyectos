using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

	public const float baseSpeed = 3.0f;
	public float speed = 3.0f;				//Variable for the speed of movement
	public float obstacleRange = 5.0f;		//variable for how far of the obstacle react

	[SerializeField] private GameObject fireBallPrefab;			//reference to fireball prefab
	private GameObject _fireBall;								//reference to fireball instance
	private bool _alive;										//Variable to check if the target is alive

	// Use this for initialization
	void Start () {
		//The target is alive when the game start
		_alive = true;
	}
	
	// Update is called once per frame
	void Update () {
		//Only move if th target is alive
		if (_alive) 
		{
			//move forward the target
			transform.Translate (0, 0, speed * Time.deltaTime);

			//A ray at the same position and same direction as the target
			Ray ray = new Ray (transform.position, transform.forward);

			RaycastHit hit;
			//Check if the ray hit any obstacle in the radius 0.75f
			if(Physics.SphereCast(ray, 0.75f, out hit))
			{
				//Get the gameObject of the hit (what the raycast hit)
				GameObject hitObject = hit.transform.gameObject;
				//Check if the ray hit the player
				if (hitObject.GetComponent<PlayerCharacter> ()) 
				{
					//If the enemy doesn't shoot already
					if (_fireBall == null) 
					{
						//Instantiate the fireball
						_fireBall = Instantiate (fireBallPrefab) as GameObject;
						//Place the fireball in front of the enemy with the same direction
						_fireBall.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
						_fireBall.transform.rotation = transform.rotation;
					}
				}
				//if the target hit a obstace in the range
				if(hit.distance < obstacleRange)
				{
					//create a random angle to change direction
					float angle = Random.Range (-110, 110);
					//Change direction in the random angle
					transform.Rotate (0, angle, 0);
				}
			}
		}
	}

	//Set _alive with alive
	public void SetAlive(bool alive)
	{
		_alive = alive;
	}

	//When the object is created, add the listener with the event and the method to call 
	void Awake()
	{
		Messenger<float>.AddListener (GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	//When the object is destroyed, remove the listener with the event and the method to call
	void OnDestroy()
	{
		Messenger<float>.RemoveListener (GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	//Set the new speed
	public void OnSpeedChanged(float value)
	{
		speed = baseSpeed * value;
	}
}
