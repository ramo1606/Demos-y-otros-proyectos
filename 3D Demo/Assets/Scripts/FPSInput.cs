using UnityEngine;
using System.Collections;

//Ensure that CharacterController component i attached
[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]

public class FPSInput : MonoBehaviour {

	public float speed = 6.0f;			//Speed of movement
	public float gravity = -9.8f;		//Gravity

	private CharacterController _charController;		//reference to the character controller component

	// Use this for initialization
	void Start () {
		//get the CharacterController component
		_charController = GetComponent<CharacterController> ();
	}

	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed;
		float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude (movement, speed);

		movement.y = gravity;

		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_charController.Move (movement);
	}
}
