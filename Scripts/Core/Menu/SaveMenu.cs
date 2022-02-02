using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    [SerializeField] private GameObject saveMenuUI;


    public void Resume()
    {
        saveMenuUI.SetActive(false);
        Time.timeScale = 1f;
        FindObjectOfType<CheckPoint>().inMenu = false;
        FindObjectOfType<PlayerMovement>().enabled = true;
        GetComponent<PauseMenu>().canPause = true;
    }


    public void SaveGame()
    {
        FindObjectOfType<GameDataManager>().SaveGame(false);     // false (stage not yet clear) when using checkpoint to save
        FindObjectOfType<SaveRecord>().UpdateText();            //　セーブメニューを使っているので、ステージはまだクリアされていないため、falseにします
    }

    public void EnterMenu()
    {
        saveMenuUI.SetActive(true);
        GetComponent<PauseMenu>().canPause = false;
        Time.timeScale = 0f;
        FindObjectOfType<PlayerMovement>().enabled = false;
    }

}
