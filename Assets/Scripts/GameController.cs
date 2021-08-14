using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
	//Sprites for the card and the two states of the oponent
	[SerializeField]
	Sprite rockSprite, paperSprite, scissorsSptite, oponentSprite, oponentWatchingSprite;

	//The oponent and his card
	[SerializeField]
	Image oponentCard, oponent;

	//The player's cards
	[SerializeField]
	GameObject[] cardObjects;

	//The four card types
	string[] cards = { "rock", "scissors", "paper" };

	//The current oponent's card
	string card;

	int score = 0;

	[SerializeField]
	TextMeshProUGUI scoreText, popUp;

	private void Start()
	{
		StartRound();
		StartCoroutine(ChangeOponentWatching());
	}

	private void StartRound()
	{
		int index = Random.Range(0, cards.Length);
		card = cards[index];
		if (card == cards[0]) //The oponent has a rock
		{
			oponentCard.sprite = rockSprite;
		}else if (card == cards[1]) //The oponent has scissors
		{
			oponentCard.sprite = scissorsSptite;
		}
		else //The opone t has paper
		{
			oponentCard.sprite = paperSprite;
		}
		//Activate card objects from last round
		foreach(GameObject go in cardObjects)
		{
			go.SetActive(true);
		}
		//Deactivate the popup message from last round
		popUp.gameObject.SetActive(false);
	}

	//Controls if you're watching oponent's card or not
	bool peeking;

	private void Update()
	{
		/*When you click the left mouse button,
		 * set peeking to true and show the oponent's card
		 */
		if (Input.GetMouseButtonDown(1))
		{
			peeking = true;
			oponentCard.enabled = true;
		}

		/*
		 * When you release the button, set peeking to false
		 * and hide oponent's card
		 */
		if (Input.GetMouseButtonUp(1))
		{
			peeking = false;
			oponentCard.enabled = false;
		}
		//If you're peeking and the oponent is watching
		if (watching && peeking)
		{
			StartCoroutine(Catch());
		}
	}
	private float waitTime = 2;
	bool watching;
	
	private IEnumerator ChangeOponentWatching()
	{
		yield return new WaitForSeconds(waitTime);
		watching = !watching;
		if (watching) //If the oponent is now watching
		{
			//Change it's sprite to the oponent watching sprite and set the wait time to 2
			waitTime = 2;
			oponent.sprite = oponentWatchingSprite;
		}
		else
		{ 
			//Change it's sprite to the oponent watching sprite and set the wait time to a random number beetwen 2 and 5
			waitTime = Random.Range(2f, 5f);
			oponent.sprite = oponentSprite;
		}
		yield return ChangeOponentWatching();
	}

	//this method is called when the user clicks on one cards
	public void PickCard(string card)
	{
		if (card == cards[0]) //You have selected a rock
		{
			if (this.card == cards[0]) //Tie
			{
				StartCoroutine(Tie());
			}else if (this.card == cards[1]) //rock beats scissors
			{
				StartCoroutine(Win());
			}
			else //Paper beats rock
			{
				StartCoroutine(Lose());
			}
		}else if (card == cards[1]) // Scissors
		{
			if (this.card == cards[0])
			{
				StartCoroutine(Lose());
			}
			else if (this.card == cards[1])
			{
				StartCoroutine(Tie());
			}
			else
			{
				StartCoroutine(Win());
			}
		}
		else //Paper
		{
			if (this.card == cards[0])
			{
				StartCoroutine(Win());
			}
			else if (this.card == cards[1])
			{
				StartCoroutine(Lose());
			}
			else
			{
				StartCoroutine(Tie());
			}
		}
	}

	private void EndRound(string text, Color textColor)
	{
		popUp.text = text;
		popUp.color = textColor;
		scoreText.text = score.ToString();
		popUp.gameObject.SetActive(true);
		foreach(GameObject go in cardObjects)
		{
			go.SetActive(false);
		}
	}

	private IEnumerator Win()
	{
		AudioManager.Instance.Play("win");
		score++;
		EndRound("You win", Color.green);
		yield return new WaitForSeconds(1.5f);
		StartRound();
	}

	private IEnumerator Lose()
	{
		AudioManager.Instance.Play("lose");
		EndRound("You lose", Color.red);
		yield return new WaitForSeconds(1.5f);
		EndGame();
	}

	private IEnumerator Tie()
	{
		AudioManager.Instance.Play("tie");
		EndRound("It's a tie", Color.white);
		yield return new WaitForSeconds(1.5f);
		StartRound();
	}

	private IEnumerator Catch()
	{
		AudioManager.Instance.Play("lose");
		EndRound("You've been caught", Color.red);
		yield return new WaitForSeconds(1.5f);
		EndGame();
	}

	private void EndGame()
	{
		if (GameManager.manager.highScore < score)
		{
			GameManager.manager.highScore = score;
			SaveSystem.Save();
		}
		score = 0;
		SceneManager.LoadScene("MainMenu");
	}
}
