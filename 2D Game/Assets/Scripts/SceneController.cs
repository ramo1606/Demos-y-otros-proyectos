using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public int _score = 0;
	public const int gridRows = 2;				//number of rows
	public const int gridCols = 4;				//number of columns
	public const float offsetX = 3f;			//offset
	public const float offsetY = 3.5f;			//offset

	[SerializeField] private MemoryCard originalCard;		//reference to the original Card
	[SerializeField] private Sprite[] images;				//Array with the images assets for the cards
	[SerializeField] private TextMesh scoreLabel;			//reference to 3D text

	private MemoryCard _firstRevealed;
	private MemoryCard _secondRevealed;

	public bool canReveal
	{
		get{ return _secondRevealed == null; }
	}

	// Use this for initialization
	void Start () {
		//The position of the original card, all the other cards will be offset from here
		Vector3 startPos = originalCard.transform.position;

		int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
		numbers = ShuffleArray (numbers);

		for (int i = 0; i < gridCols; i++) 
		{
			for (int j = 0; j < gridRows; j++) 
			{
				//a reference for the card
				MemoryCard card;
				//if the position is the first card, so is the original card
				if (i == 0 && j == 0) {
					card = originalCard;
				} else 
				{
					//otherwise instantiate a new card form the original
					card = Instantiate (originalCard) as MemoryCard;
				}

				int index = j * gridCols + i;
				int id = numbers[index];
				card.SetCard (id, images[id]);

				//Set the position of the new card
				float posX = (offsetX * i) + startPos.x;
				float posY = (offsetY * j) + startPos.y;
				card.transform.position = new Vector3 (posX, posY, startPos.z);

			}
		}
	}

	//implementation of knuth shuffle algorithm
	private int[] ShuffleArray(int[] numbers)
	{
		int[] newArray = numbers.Clone () as int[];
		for (int i = 0; i < newArray.Length; i++) 
		{
			int tmp = newArray [i];
			int r = Random.Range (i, newArray.Length);
			newArray [i] = newArray [r];
			newArray [r] = tmp;
		}
		return newArray;
	}

	public void CardRevealed(MemoryCard card)
	{
		if (_firstRevealed == null) 
		{
			_firstRevealed = card;
		}else 
		{
			_secondRevealed = card;
			StartCoroutine (CheckMatch());
		}
	}

	private IEnumerator CheckMatch()
	{
		if (_firstRevealed.id == _secondRevealed.id) {
			_score++;
			scoreLabel.text = "score: " + _score;
		} else 
		{
			yield return new WaitForSeconds(.5f);

			_firstRevealed.Unrevealed();
			_secondRevealed.Unrevealed();
		}
		_firstRevealed = null;
		_secondRevealed = null;
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
