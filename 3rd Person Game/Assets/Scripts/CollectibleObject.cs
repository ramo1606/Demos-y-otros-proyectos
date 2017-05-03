using UnityEngine;
using System.Collections;

public class CollectibleObject : MonoBehaviour {

	[SerializeField]private string itemName;

	void OnTriggerEnter(Collider other)
	{
		Managers.Inventory.AddItem (this.itemName);
		Destroy (this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
