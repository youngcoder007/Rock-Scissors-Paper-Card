[System.Serializable]
public class GameData
{
	public int highScore;
	public GameData(GameManager manager)
	{
		highScore = manager.highScore;
	}
}
