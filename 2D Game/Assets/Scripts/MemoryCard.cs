using UnityEngine;
using System.Collections;

public class MemoryCard : MonoBehaviour {

	[SerializeField] private GameObject cardBack;			//Reference to a cardback
	[SerializeField] private SceneController controller;	//Reference to the SceneController 

	private int _id;

	public int id
	{
		get { return _id; }
	}

	//Set the sprite for a new card
	public void SetCard(int id, Sprite image)
	{
		_id = id;
		GetComponent<SpriteRenderer> ().sprite = image;
	}

	//Method when we click
	public void OnMouseDown()
	{
		//if the cardBack is active and if is posible to reveal another card
		if (cardBack.activeSelf && controller.canReveal) 
		{
			//deactivate the cardBack
			cardBack.SetActive (false);
			//Notify the controller that this card is revealed
			controller.CardRevealed (this);
		}
	}

	//method to turning the card back
	public void Unrevealed()
	{
		cardBack.SetActive (true);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
