using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour, IGameManager {
	public ManagerStatus status { get; private set; }
	public string equipedItem{ get; private set; }

	private Dictionary<string, int> _items;
	private NetworkService _network;

	// Use this for initialization
	public void Startup (NetworkService service) 
	{
		Debug.Log ("Inventory Manager Starting...");
		_network = service;
		UpdateData (new Dictionary<string, int> ());
		status = ManagerStatus.Started;
	}

	private void DisplayItems()
	{
		string itemDisplay = "items: ";
		foreach (KeyValuePair<string, int> item in _items) 
		{
			itemDisplay += item.Key + "(" + item.Value + ")";
		}
		Debug.Log (itemDisplay);
	}

	public void AddItem(string item)
	{
		if (_items.ContainsKey (item))
			_items[item] += 1;
		else
			_items.Add(item, 1);

		DisplayItems ();
	}

	public List<string> GetItemList()
	{
		List<string> list = new List<string> (_items.Keys);
		return list;
	}

	public int GetItemCount(string name)
	{
		if (_items.ContainsKey (name))
			return _items [name];
		return 0;
	}

	public bool EquipItem(string name)
	{
		if (_items.ContainsKey (name) && equipedItem != name) 
		{
			equipedItem = name;
			return true;
		}

		equipedItem = null;

		return false;
	}

	public bool ConsumeItem(string item)
	{
		if (_items.ContainsKey (name)) {
			_items [name]--;
			if (_items [name] == 0)
				_items.Remove (name);
		} else 
		{
			return false;
		}

		return true;
	}

	public void UpdateData(Dictionary <string, int> items)
	{
		_items = items;
	}

	public Dictionary<string, int> GetData ()
	{
		return _items;
	} 
}
