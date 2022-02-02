using UnityEngine;

public class CheckPoint : Hint
{
    private bool canSave;
    [System.NonSerialized] public bool inMenu;
    private bool flagOpened;

    private void Update()
    {
        //　Show save menu when player is inside checkpoint area and presses U button   
        //  プレイヤーがチェックポイントにいて、Uボタンをしたらセーブメニューを表示します
        if (canSave && !FindObjectOfType<PauseMenu>().GameIsPaused && !inMenu)  {
            if (Input.GetKeyDown(KeyCode.U)) {
                inMenu = true;
                FindObjectOfType<SaveMenu>().EnterMenu();
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag == "Player") {
            collision.GetComponent<PlayerHealth>().AddHealth(5f);       // recover player HP　プレイヤーがチェックポイントに触れたら、HPを全回復させてからボス戦に挑ませます。
            canSave = true;
            if (!flagOpened)
                GetComponent<Animator>().SetTrigger("saved");
        }
            
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if (collision.tag == "Player")
            canSave = false;
    }
}
