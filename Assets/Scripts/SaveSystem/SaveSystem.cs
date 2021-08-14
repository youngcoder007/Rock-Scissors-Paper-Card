using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
	public static readonly string path = Application.persistentDataPath + "/game.sav";

	public static void Save()
	{
		//Creates a BinaryFormatter
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(path, FileMode.Create);
		GameData data = new GameData(GameManager.manager);
		//Saves the game data in a binary data
		formatter.Serialize(stream, data);
		stream.Close();
	}

	public static GameData Load()
	{
		if (!File.Exists(path))
		{
			//If the file was not yet created, return nothing
			return null;
		}
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream stream = new FileStream(path, FileMode.Open);
		GameData data = formatter.Deserialize(stream) as GameData;
		stream.Close();
		return data;
	}
}