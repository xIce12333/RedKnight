using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [SerializeField] private TextMeshProUGUI text; 
    [System.NonSerialized] public int coinNum;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void ChangeNum(int coinValue)            // for collecting coins     コインを拾ったときにコイン数を変えます
    {
        coinNum += coinValue;
        text.text = ": " + coinNum.ToString();
    }
    public void SetStartCoinNum(int _coinNum)        // set initial coin num for loading data or transition to new scene
    {                                                // データロード時のコイン数を調整します
        text.text = ": " + _coinNum.ToString();
        coinNum = _coinNum;
    }
}
