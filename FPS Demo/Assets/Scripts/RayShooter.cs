using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RayShooter : MonoBehaviour {
	[SerializeField] private AudioSource soundSource;
	[SerializeField] private AudioClip hitWallSound;
	[SerializeField] private AudioClip hitEnemySound;
 
	//reference to camera
	private Camera _camera;
	// Use this for initialization
	void Start () {
		//Get the camera component
		_camera = GetComponent<Camera> ();

		//Lock the mouse cursor at the center of the screen
		Cursor.lockState = CursorLockMode.Locked;
		//hide the cursor
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		//if click is pressed and the cursor is not over a UI component
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) 
		{
			//point is the center of the screen, half of the width and half o the height
			Vector3 point = new Vector3 (_camera.pixelWidth/2, _camera.pixelHeight/2, 0);
			//Create the ray from that point
			Ray ray = _camera.ScreenPointToRay (point);
			//Where the ray hits
			RaycastHit hit;

			//if we hit something
			if(Physics.Raycast(ray, out hit))
			{
				//Retrieve the object the ray hit
				GameObject hitObject = hit.transform.gameObject;
				//Get the ReactiveTarget component
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget> ();

				if (target != null) {
					//If the ray hit a target, then the target has to react
					target.ReactToHit ();
					//Broadcast the message that the enemy was hit, so the UI can react
					Messenger.Broadcast (GameEvent.ENEMY_HIT);

					soundSource.PlayOneShot (hitEnemySound);
				} else {
					//Start the coroutine for the place where the ray hits
					StartCoroutine (SphereIndicator (hit.point));
					soundSource.PlayOneShot (hitWallSound);
				}
			}
		}

		if (Input.GetKeyDown (KeyCode.Escape))
			Cursor.visible = true;
	}

	//We use coroutines to simulate asynchronous functions, the yield return makes that the 
	//program continue executing while we waits 1 second
	private IEnumerator SphereIndicator(Vector3 pos)
	{
		//Create the sphere to indicate the place where the ray hits
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		//Set the position to where the ray hits
		sphere.transform.position = pos;

		//Wait 1 second
		yield return new WaitForSeconds (1.5f);

		//Destroy the sphere
		Destroy (sphere);
	}

	void OnGUI()
	{
		int size = 12;
		float posX = _camera.pixelWidth/2 - size/4;
		float posY = _camera.pixelHeight/2 - size/2;

		//Display a label in the screen after the 3D scene is rendered.
		GUI.Label(new Rect(posX, posY, size, size), "*");
	}
}
