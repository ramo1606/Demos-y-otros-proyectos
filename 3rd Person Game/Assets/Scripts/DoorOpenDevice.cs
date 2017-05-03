using UnityEngine;
using System.Collections;

public class DoorOpenDevice : MonoBehaviour {

	[SerializeField] private Vector3 dPos;				//Position to offset to when the door opens

	private bool _open;

	public void Operate()
	{
		if (_open) {
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
		} else 
		{
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
		}
		_open = !_open;
	}

	public void Activate()
	{
		if (!_open) 
		{
			Vector3 pos = transform.position + dPos;
			transform.position = pos;
			_open = true;
		}
	}

	public void Deactivate()
	{
		if (_open) 
		{
			Vector3 pos = transform.position - dPos;
			transform.position = pos;
			_open = false;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
