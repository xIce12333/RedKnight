using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public int level;
    public bool levelCleared;
    public int coinNum;
    public float[] playerPosition;
    public string saveTime;

    
    // for checkpoint saving　　チェックポイントでのセーブ用
    public SaveData(int _level, bool _levelCleared ,int _coinNum, Vector2 _playerPosition)
    {
        level = _level;
        levelCleared = _levelCleared;
        coinNum = _coinNum;
        playerPosition = new float[2];
        playerPosition[0] = _playerPosition.x;
        playerPosition[1] = _playerPosition.y;
        saveTime = System.DateTime.UtcNow.ToLocalTime().ToString("dd / MM / yyyy  HH:mm");
    }

    // for saving after stage clear     ステージクリア時のセーブ用
    public SaveData(int _level, bool _levelcleared, int _coinNum)
    {
        level = _level;
        levelCleared = _levelcleared;
        coinNum = _coinNum;
        saveTime = System.DateTime.UtcNow.ToLocalTime().ToString("dd / MM / yyyy  HH:mm");
    }
}
