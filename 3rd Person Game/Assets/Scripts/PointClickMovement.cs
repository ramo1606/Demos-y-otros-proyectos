using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class PointClickMovement : MonoBehaviour {

	[SerializeField] private Transform target;			//reference to the target(camera)

	public float jumpSpeed = 15.0f;						//speedjump
	public float gravity = -9.8f;						//gravity
	public float terminalVelocity = -10.0f;				//max gravity
	public float minFall = -1.5f;						//
	public float pushForce = 3.0f;
	public float deceleration = 20.0f;
	public float targetBuffer = 1.5f;

	public float rotSpeed = 15.0f;						//rotation speed
	public float moveSpeed = 6.0f;						//movement speed

	private CharacterController _controller;			//reference to the CharacterController component
	private ControllerColliderHit _contact;				//variable to store collision data
	private float _vertSpeed;							//vertical speed
	private bool _firstJump;
	private Animator _animator;
	private float _curSpeed = 0f;
	private Vector3 _targetPos = Vector3.one;

	// Use this for initialization
	void Start () 
	{
		_firstJump = false;
		//whe you are in the ground vertical speed is not 0, is min fall to react to the ground
		_vertSpeed = minFall;
		_controller = GetComponent<CharacterController> ();
		_animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () 
	{
		Vector3 movement = Vector3.zero;
		float horInput = Input.GetAxis ("Horizontal");
		float vertInput = Input.GetAxis ("Vertical");
		bool hitGround = false;
		RaycastHit hit;

		if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
		{
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit mouseHit;
			if(Physics.Raycast(ray, out mouseHit))
			{
				GameObject hitObject = mouseHit.transform.gameObject;
				if (hitObject.layer == LayerMask.NameToLayer ("Ground")) 
				{
					_targetPos = mouseHit.point;
					_curSpeed = moveSpeed;
				}
			}
		}

		if (_targetPos != Vector3.one) 
		{
			Vector3 adjustPos = new Vector3 (_targetPos.x, transform.position.y, _targetPos.z);
			Quaternion targetRot = Quaternion.LookRotation (adjustPos - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, rotSpeed * Time.deltaTime);

			movement = _curSpeed * Vector3.forward;
			movement = transform.TransformDirection (movement);

			if(Vector3.Distance(_targetPos, transform.position) > targetBuffer)
			{
				_curSpeed -= deceleration * Time.deltaTime;
				if (_curSpeed <= 0) 
				{
					_targetPos = Vector3.one;
				}
			}
		}
		_animator.SetFloat("speed", movement.sqrMagnitude);

		//Only handle movement when the keys are pressed
		/*if (horInput != 0 || vertInput != 0) 
		{
			movement.x = horInput * moveSpeed;
			movement.z = vertInput * moveSpeed;

			//clamp the movement to moveSpeed for diagonal movement
			movement = Vector3.ClampMagnitude (movement, moveSpeed);

			//keep the original rotation to restore after the movement
			Quaternion tmp = target.rotation;
			//Adjust the rotation so only rotates in Y-axis
			target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
			//Transform movement direction from local to global coordinates
			movement = target.TransformDirection (movement);
			//Restores camera rotation
			target.rotation = tmp;

			//calculate a rotation facing in that direction
			//transform.rotation = Quaternion.LookRotation (movement);

			//Use lerp to smooth rotation movement
			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp(transform.rotation,
				direction, rotSpeed * Time.deltaTime);

			_animator.SetFloat ("speed", movement.sqrMagnitude);
		}*/

		//check if the player is falling
		if (_vertSpeed < 0 && Physics.Raycast (transform.position, Vector3.down, out hit)) 
		{
			float check = (_controller.height + _controller.radius) / (1.9f);
			hitGround = hit.distance <= check - _controller.center.y;
		}

		//Check if the character is in the ground
		if (hitGround) 
		{
			//if press jump button
			if (Input.GetButtonDown ("Jump")) 
			{
				//set the vertical speed to jump speed and set _first jump to true
				_vertSpeed = jumpSpeed;
				//_firstJump = true;
				_animator.SetBool ("jumping", true);
			} else 
			{
				//otherwise _vertspeed stay minfall
				_vertSpeed = minFall;
				_animator.SetBool ("jumping", false);
			}
		} else 
		{
			//if the character is in the air and press jump button again we do a double jump
			/*if (Input.GetButtonDown ("Jump")) 
			{
				//if we already did a jump we can do another one
				if (_firstJump) 
				{
					_vertSpeed = jumpSpeed;
					_firstJump = false;
					_animator.SetBool ("DoubleJump", true);
				} 
			}*/
			//When we are falling we set the vertical velocity to  gravity * 5 
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity) 
			{
				_vertSpeed = terminalVelocity;
			}

			if (_contact != null ) {
				_animator.SetBool("jumping", true);
			}

			if (_controller.isGrounded) 
			{
				if (Vector3.Dot (movement, _contact.normal) < 0) 
				{
					movement = _contact.normal * moveSpeed;
				} else 
				{
					movement += _contact.normal * moveSpeed;
				}
			}
			_animator.SetBool ("jumping", false);
		}

		//set the movement Y to the vertical speed
		movement.y = _vertSpeed;

		//Multiply for deltatime to maintain movement frame independent
		movement *= Time.deltaTime;

		//move the character
		_controller.Move (movement);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_contact = hit;
		Rigidbody body = hit.collider.attachedRigidbody;

		if (body != null && !body.isKinematic)
			body.velocity = hit.moveDirection * pushForce;
	}
}
