﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicUI : MonoBehaviour {

	void OnGUI()
	{
		int posX = 10;
		int posY = 10;
		int width = 100;
		int height = 30;
		int buffer = 10;

		List<string> itemList = Managers.Inventory.GetItemList();
		if (itemList.Count == 0)
			GUI.Box (new Rect (posX, posY, width, height), "No items");

		foreach (string item in itemList) 
		{
			int count = Managers.Inventory.GetItemCount (item);
			Texture2D image = Resources.Load<Texture2D> ("Icons/"+item);
			GUI.Box (new Rect (posX, posY, width, height), new GUIContent ("(" + count + ")", image));
			posX += width + buffer;
		}

		string equiped = Managers.Inventory.equipedItem;
		if (equiped != null) 
		{
			posX = Screen.width - (width + buffer);
			Texture2D image = Resources.Load<Texture2D> ("Icons/" + equiped) as Texture2D;
			GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
		}

		posX = 10;
		posY += height + buffer;

		foreach (string item in itemList) 
		{
			if (item == "Health") 
			{
				if (GUI.Button (new Rect (posX, posY, width, height), "Use " + item)) 
				{
					Managers.Inventory.ConsumeItem (item);
					Managers.Player.ChangeHealth (25);
				}
			}else if (GUI.Button (new Rect (posX, posY, width, height), "Equip " + item)) 
			{
				Managers.Inventory.EquipItem (item);
			}
				
			posX += width + buffer;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
