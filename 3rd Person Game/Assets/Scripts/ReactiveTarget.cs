using UnityEngine;
using System.Collections;

public class ReactiveTarget : MonoBehaviour {

	public void ReactToHit()
	{
		//get the AI component
		WanderingAI behavior = GetComponent<WanderingAI> ();
		if (behavior != null)
			//Set alive to false, the player hit the target
			behavior.SetAlive (false);
		//Start the Die coroutine
		StartCoroutine (Die());
	}

	private IEnumerator Die()
	{
		//Topple the enemy
		transform.Rotate (-75, 0, 0);

		//wait 2 seconds
		yield return new WaitForSeconds (2f);

		//Destroy the object(target)
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
