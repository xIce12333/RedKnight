using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMenu : MonoBehaviour
{
    private bool saved;
    public void SaveGame()
    {
        saved = true;
        FindObjectOfType<GameDataManager>().SaveGame(true);        // true (stage clear) when saving after a clearing certain stage
        FindObjectOfType<SaveRecord>().UpdateText();               //　このNextMenuスクリプトはステージクリア用なので、trueにします
        Invoke("NextButton", 1f);       // go to next level after saving    セーブしたら次のシーンに移ります
    }

    public void NextButton()    // load next level  次のシーンに移ります
    {
        if (saved == false) {
            PlayerPrefs.SetInt("UnsavedCoin", CoinManager.instance.coinNum);
            saved = true;
        }
            
        if (SceneManager.GetActiveScene().name == "Castle")     //　ゲームをクリアしたら、メインメニューに戻ります
            SceneManager.LoadScene("MainMenu");
        else 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
