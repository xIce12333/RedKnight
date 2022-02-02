using UnityEngine;
using UnityEngine.UI;

public class SaveRecord : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Text dateText;
    [SerializeField] private Text stageText;
    [SerializeField] private Text coinText;
    [SerializeField] private Text deathText;

    private void Awake()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        SaveData data = SaveSystem.LoadGame();   
        if (data == null) {         // no save data     
            DefaultText();
            return;
        }      
            
        
        //  save data found, update text    セーブデータが見つかった場合、テキストを変えます
        dateText.text = data.saveTime;
        UpdateStageText(data);
        coinText.text = data.coinNum.ToString();
        deathText.text = PlayerPrefs.GetInt("DieNum", 0).ToString();
    }

    public void DefaultText()       // セーブデータが見つからなかった場合、デフォルトのテキストを表示します
    {
        dateText.text = "セーブデータがありません";
        stageText.text = "セーブデータがありません";
        coinText.text = "セーブデータがありません";
        deathText.text = "セーブデータがありません";
    }

    private void UpdateStageText(SaveData data)
    {
        if (data.level == 1)
            stageText.text = "プロローグ";
        else if (data.level == 2)        // Slime Forest    スライムの森
            stageText.text = "スライムの森";
        else if (data.level == 3)      // Castle        試練の間
            stageText.text = "試練の間";

        // stage progress       
        if (data.levelCleared)
            stageText.text += "     クリア";
        else
            stageText.text += "     ボス前";
    }
}
