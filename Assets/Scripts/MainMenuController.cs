using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI scoreText;

	public GameObject splashScreen, mainMenu;

	private IEnumerator Start()
	{
		AudioManager.Instance.Play("splash");
		scoreText.text = 
			string.Format("High score: {0}", GameManager.manager.highScore);
		yield return new WaitForSeconds(2.5f);
		splashScreen.SetActive(false);
		mainMenu.SetActive(true);
	}

    private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			SceneManager.LoadScene("Game");
		}
	}
}
