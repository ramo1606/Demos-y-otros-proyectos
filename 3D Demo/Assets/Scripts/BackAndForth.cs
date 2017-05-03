using UnityEngine;
using System.Collections;

public class BackAndForth : MonoBehaviour {

	public float speed = 3.0f;
	public float minZ = -16.0f;			//The sphere will move between this two positions
	public float maxZ = 16.0f;

	private int _direction = 1;			//Direction of the sphere

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Move the sphere
		transform.Translate (0, 0, _direction * speed * Time.deltaTime);

		//Variable to check if the sphere bounce
		bool bounced = false;

		//if the sphere is out of the boundaries
		if(transform.position.z > maxZ || transform.position.z < minZ)
		{
			//change direction
			_direction = -_direction;
			bounced = true;
		}

		//if the sphere bounce
		if (bounced)
			//Move the sphere in the oposite direction
			transform.Translate (0, 0, _direction * speed * Time.deltaTime);
	}
}
