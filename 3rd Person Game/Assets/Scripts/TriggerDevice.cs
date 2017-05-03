using UnityEngine;
using System.Collections;

public class TriggerDevice : MonoBehaviour {
	
	[SerializeField] private GameObject[] targets;

	public bool requireKey;

	void OnTriggerEnter(Collider other)
	{
		if(requireKey && Managers.Inventory.equipedItem != "Key")
			return;
		
		foreach(GameObject target in targets)
		{
			target.SendMessage ("Activate");
		}
	}

	void OnTriggerExit(Collider other)
	{
		foreach(GameObject target in targets)
		{
			target.SendMessage ("Deactivate");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
