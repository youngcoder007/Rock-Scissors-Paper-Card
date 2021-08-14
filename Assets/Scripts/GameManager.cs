using UnityEngine;

public class GameManager : MonoBehaviour
{
	//Global instance of the game
	public static GameManager manager;
	public int highScore;

	private void Awake()
	{
		if (manager == null)
		{
			manager = this;
			DontDestroyOnLoad(this);
			//Load game
			GameData data = SaveSystem.Load();
			if (data != null)
			{
				highScore = data.highScore;
			}
		}
		else
		{
			Destroy(gameObject);
		}
	}

}
