using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour {

	//Enum data structure to associate names with settings
	public enum RotationAxes
	{
		MouseXAndY = 0,
		MouseX = 1,
		MouseY = 2
	}

	[SerializeField] private Camera _camera;

	public RotationAxes axes = RotationAxes.MouseXAndY;		//set rotation axes to access from editor

	public float sensitivityHor = 7.0f;						//speed of horizontal rotation
	public float sensitivityVert = 7.0f;					//speed of vertical rotation

	public float minimumVert = -45.0f;						//angle to limit vertical rotation
	public float maximumVert = 45.0f;						//angle to limit vertical rotation

	private float _rotationX = 0;							//Vertical angle

	// Use this for initialization
	void Start () 
	{
		//Get the rigidbody component of the character
		Rigidbody body = GetComponent<Rigidbody> ();
		//if get the body
		if (body != null)
			//set freezeRotation true to make the character not affected by other physics other than mouse movement
			body.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if the mouse move on the x axis
		if(axes == RotationAxes.MouseX)
		{
			//rotate the transform in the mouse x position by the sensitvityHor(speed)
			transform.Rotate (0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
		}
		else if(axes == RotationAxes.MouseY)
		{
			//Increment the vertical angle based on the mouse
			_rotationX -= Input.GetAxis ("Mouse Y") * sensitivityVert;
			//Clamp the vertical angle between min and max angle
			_rotationX = Mathf.Clamp (_rotationX, minimumVert, maximumVert);

			//keep the same Y angle (No horizontal rotation)
			float rotationY = transform.localEulerAngles.y;

			//Create new vector from the new angles
			transform.localEulerAngles = new Vector3 (_rotationX, rotationY, 0);
		}
		else
		{
			//Increment the vertical angle based on the mouse
			_rotationX -= Input.GetAxis ("Mouse Y") * sensitivityVert;
			//Clamp the vertical angle between min and max angle
			_rotationX = Mathf.Clamp (_rotationX, minimumVert, maximumVert);

			//Delta is how much the angle for Y change 
			float delta = Input.GetAxis ("Mouse X") * sensitivityHor;

			//increment the Y angle by delta
			float rotationY = transform.localEulerAngles.y + delta;

			//Create new vector from the new angles, just move the player
			transform.localEulerAngles = new Vector3 (0, rotationY, 0);
			//Move the camera only, up and down.
			_camera.transform.localEulerAngles = new Vector3(_rotationX, 0, 0);
		}
	}
}
