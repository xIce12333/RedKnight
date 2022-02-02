using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDataManager : MonoBehaviour
{
    private PlayerHealth player;
    private CoinManager coinManager;


    private void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();
        coinManager = FindObjectOfType<CoinManager>();
        Time.timeScale = 1f;
        Physics2D.IgnoreLayerCollision(8, 11, false);   
        Physics2D.IgnoreLayerCollision(8, 3, false);
    }
    private void Start()        // Start is called after Awake, prevent null reference  
    {       
        LoadData();
    }

    private void LoadData()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
            return;

        SaveData data = SaveSystem.LoadGame();
  //      int isLoading = PlayerPrefs.GetInt("isLoading", 0);     // for identifying loading data or starting new game    プレイヤーがデータをロードしているか、最初からやり直しているかを区別するための変数

        int coinLastlevel = PlayerPrefs.GetInt("UnsavedCoin", -1);
        if (coinLastlevel != -1) {          // player didn't save in last stage
            coinManager.SetStartCoinNum(coinLastlevel);
            return;
        }
        else if (coinLastlevel == -1 && SceneManager.GetActiveScene().name == "Prologue") {
            coinManager.SetStartCoinNum(0);
            return;
        }

        if (data != null) {
            if (data.playerPosition != null)
                player.transform.position = new Vector2(data.playerPosition[0], data.playerPosition[1]);
        }

        PlayerPrefs.DeleteKey("UnsavedCoin");
        if (data == null) 
            coinManager.SetStartCoinNum(0);
        else
            coinManager.SetStartCoinNum(data.coinNum);
   //     PlayerPrefs.SetInt("isLoading", 0);
        
    }

    public void SaveGame(bool _levelCleared)            // Save Game
    {
        PlayerPrefs.DeleteKey("UnsavedCoin");
        SaveData data;
        int level = SceneManager.GetActiveScene().buildIndex;
        int coinNum = coinManager.coinNum;
        if (!_levelCleared) {        // saving at checkpoint, need to save player position     プレイヤーがチェックポイントでセーブしているため、キャラクターの位置を記録する必要があります。
            Vector2 playerPosition = player.transform.position;
            data = new SaveData(level, _levelCleared, coinNum, playerPosition);
        }
        else {                      // end of stage saving      ステージをクリアしたときにセーブ
            data = new SaveData(level, _levelCleared, coinNum);
        }
        SaveSystem.SaveGame(data);
    }

    public void LoadGame()
    {
        PlayerPrefs.DeleteKey("UnsavedCoin");
        SaveData data = SaveSystem.LoadGame();
    //    PlayerPrefs.SetInt("isLoading", 1);
        if (data == null)       // no save data found
            return;     
        else if (data.levelCleared && data.level == 3)      // game cleared, don't load game    ゲームをクリアした場合、そのゲームデータをロードできないようにします。
            return;
        if (data.levelCleared)
            FindObjectOfType<LevelLoader>().LoadLevel(data.level + 1);
        else
            FindObjectOfType<LevelLoader>().LoadLevel(data.level);
    }



    public void DeleteSaveData()
    {
        SaveSystem.DeleteData();
    }
}
