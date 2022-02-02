using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject nextMenuUI;
    private float sceneTransitionTime = 2f;



    // public for other script to load level 
    public void LoadNextLevel()             // for stage clear  ステージをクリアした場合用の関数     
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")       // if it is main menu, don't show the save menu 
            StartCoroutine(LoadLevelWithoutSave());                 // ゲームを起動した直後、セーブ画面を表示しないようにします。
        else
            StartCoroutine(LoadNext());
    }

    public void LoadLevel(int levelIndex)       // for loading data
    {
        StartCoroutine(Load(levelIndex));
    }

    private IEnumerator LoadNext()
    {
        animator.SetTrigger("nextLevel");    
        StopPlayer();
        yield return new WaitForSecondsRealtime(sceneTransitionTime);
        Time.timeScale = 1f;        
        nextMenuUI.SetActive(true);
    }
    public IEnumerator LoadLevelWithoutSave()       // MainMenu用
    {
        animator.SetTrigger("nextLevel");
        yield return new WaitForSecondsRealtime(sceneTransitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator Load(int levelIndex)
    {
        animator.SetTrigger("nextLevel");
        if (player != null)         
            StopPlayer();
        yield return new WaitForSecondsRealtime(sceneTransitionTime);
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelIndex);
    }

    private void StopPlayer()       // stop player for scene transition     データロード中はキャラクターの動きを止めます。
    {
        player.GetComponent<PlayerMovement>().StopPlayerMovement();
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<PlayerAttack>().enabled = false;
        player.GetComponent<PlayerAnimation>().enabled = false;
    }
    
}
