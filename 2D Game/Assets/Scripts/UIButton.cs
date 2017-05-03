﻿using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour {

	[SerializeField] private GameObject targetObject;		//Reference to the object that we want to send the message
	[SerializeField] private string targetMessage;			//Message
	public Color highlightColor = Color.red;				//Color to highlight the button

	public void OnMouseOver()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		if (sprite != null)
			sprite.color = highlightColor;
	}

	public void OnMouseExit()
	{
		SpriteRenderer sprite = GetComponent<SpriteRenderer> ();
		if (sprite != null)
			sprite.color = Color.white;
	}

	public void OnMouseDown()
	{
		transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
	}

	public void OnMouseUp()
	{
		transform.localScale = Vector3.one;
		if (targetObject != null)
			targetObject.SendMessage (targetMessage);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
