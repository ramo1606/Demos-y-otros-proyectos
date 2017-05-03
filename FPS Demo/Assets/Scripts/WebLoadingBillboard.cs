using UnityEngine;
using System.Collections;

public class WebLoadingBillboard : MonoBehaviour {

	public void Operate()
	{
		Managers.Images.GetWebImage (OnWebImage);
	}
		
	private void OnWebImage(Texture2D image)
	{
		GetComponent<Renderer> ().material.mainTexture = image;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
