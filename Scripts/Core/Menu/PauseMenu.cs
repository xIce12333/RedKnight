using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool GameIsPaused;
    public bool canPause = true;
    [SerializeField] private GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canPause) {
            if (GameIsPaused)           // game is already paused, resume the game
                Resume();               //　ゲームがすでに中止されている場合、ゲームを続けます
            else                    // pause the game
                Pause();            //　ゲームを中止します
        }
    }


    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        FindObjectOfType<PlayerMovement>().enabled = true;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        FindObjectOfType<PlayerMovement>().enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //---------SFX-----------//

    public void PlayHoverSound()
    {
        SoundManager.instance.PlaySound("Hover");
    }

    public void PlayClickSound()
    {
        SoundManager.instance.PlaySound("Click");
    }
}
