using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	public float speed = 10.0f;			//Speed of the fireball
	public int damage = 1;				//Damage of the fireball

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Move the fireball
		transform.Translate (0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		//Get the PlayerCharacter reference
		PlayerCharacter player = other.GetComponent<PlayerCharacter> ();
		//Check if the collision was the player
		if (player != null) 
		{
			player.Hurt (damage);
		}
		//destroy the fireball
		Destroy (gameObject);
	}
}
