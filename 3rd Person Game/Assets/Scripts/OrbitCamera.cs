using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour {

	[SerializeField] private Transform target;		//reference to the target(character)
	[SerializeField] private Transform targetLookAt;

	public float rotSpeed;							//rotation speed

	private float _rotY;
	private Vector3 _offset;

	// Use this for initialization
	void Start () 
	{
		//Angle of the camera
		_rotY = transform.eulerAngles.y;
		//store the starting position offset between the target and the camera
		_offset = target.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Update after all the objects in the scene are updated
	void LateUpdate()
	{
		/*float horInput = Input.GetAxis ("Horizontal");
		//if rotate the camera slowly with the keys
		if (horInput != 0)
			_rotY -= horInput * rotSpeed;
		//Or rotate the camera fast with mouse
		else
			_rotY += Input.GetAxis ("Mouse X") * rotSpeed * 3;*/

		_rotY -= Input.GetAxis("Horizontal") * rotSpeed;
		//transform the rotation to a quaternion
		Quaternion rotation = Quaternion.Euler (0, _rotY, 0);

		//multiply the rotation with the offset to get the rotated offset position, 
		//then substract to the target position to get the position relative to the target.
		transform.position = target.position - (rotation * _offset);

		//the camera always look at the target
		transform.LookAt (targetLookAt);
	}
}
