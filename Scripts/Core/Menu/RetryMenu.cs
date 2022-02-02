using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryMenu : MonoBehaviour
{
    [SerializeField] private GameObject retryMenuUI;


    public void ShowRetryMenu()
    {
        retryMenuUI.SetActive(true);
        GetComponent<PauseMenu>().canPause = false;
        Time.timeScale = 0f;
        SoundManager.instance.PlaySoundIfNotPlaying("Game Over");
        SoundManager.instance.StopSound("BGM");
        SoundManager.instance.StopSound("Boss");

    }

    public void Retry()
    {
        FindObjectOfType<GameDataManager>().LoadGame();
    }

    
    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
