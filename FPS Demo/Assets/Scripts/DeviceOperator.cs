﻿using UnityEngine;
using System.Collections;

public class DeviceOperator : MonoBehaviour {

	public float radius = 1.5f;				//how far away from the player to activate device

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Fire3"))
		{
			Collider[] hitColliders = Physics.OverlapSphere (transform.position, radius);
			foreach (Collider hitCollider in hitColliders) 
			{
				Vector3 direction = hitCollider.transform.position - transform.position;
				//only open the door if the character is facing the door
				if(Vector3.Dot(transform.forward, direction) > .5f)
					hitCollider.SendMessage ("Operate", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
