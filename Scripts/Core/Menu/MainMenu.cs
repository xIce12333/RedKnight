using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        int ran = Random.Range(0, 2);           // Play random BGM  ランダムのBGMを再生します
        if (ran == 0)           
            FindObjectOfType<SoundManager>().PlaySound("Menu BGM"); 
        else
            FindObjectOfType<SoundManager>().PlaySound("Menu BGM2");
    }
    public void Playgame()
    {
        PlayerPrefs.SetInt("isLoading", -1);
        FindObjectOfType<LevelLoader>().LoadNextLevel();
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }

    public void HoverSound()
    {
        SoundManager.instance.PlaySound("Hover");
    }
    
    public void ClickSound()
    {
        SoundManager.instance.PlaySound("Click");
    }
}
