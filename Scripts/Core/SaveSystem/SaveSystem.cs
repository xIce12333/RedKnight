using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;


// Use Binary Formatter for saving and loading, enhance security
// ゲームデータを簡単に改造させないため、BinaryFormatterを使います
public static class SaveSystem 
{
    public static void SaveGame(SaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string savePath = FindPath();
        FileStream file = File.Create(savePath);
        formatter.Serialize(file, data);
        file.Close();
    }

    public static SaveData LoadGame()
    {
        string savePath = FindPath();
        if (File.Exists(savePath)) {        // file found   セーブデータが見つかった場合のみ、ロードします
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            SaveData data = formatter.Deserialize(file) as SaveData; // cast the data as SaveData
            file.Close();
            return data;
        }
        else {              // file not found, load error       セーブデータが見つからなかった場合、nullを返す
            return null;
        }
    }

    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
        string savePath = FindPath();
        if (File.Exists(savePath)) 
            File.Delete(savePath);
    }

    private static string FindPath()
    {
        return Application.persistentDataPath + "/savedata.sav";    
    }
    
}
