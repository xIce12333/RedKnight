using UnityEngine;
using System.Collections;
public class PlayerHealth : Health
{
    public bool isDead {get; private set;}
    private SpriteRenderer render; 
    public override void Awake()
    {
        base.Awake();
        render = GetComponent<SpriteRenderer>();
        SaveData data = SaveSystem.LoadGame();

    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);

        if (GetCurrentHealth() > 0) {
            SoundManager.instance.PlaySound("Player Hurt");
            StartCoroutine(Invulnerability());       
        }
        else {
            if (!isDead) {
                GetComponent<PlayerMovement>().enabled = false;
                SoundManager.instance.PlaySound("Player Death");
                PlayerPrefs.DeleteKey("UnsavedCoin");       // for player not saving
                SoundManager.instance.StopSound("On Wall");
                UpdateDieNum();
                isDead = true;
            }
        }
    }
    private void UpdateDieNum()     // update the number of times that the player died for save record
    {                               // プレイヤーが死んだ回数を記録し、データを消さない限りこの回数は蓄積します。
        int dieNum = PlayerPrefs.GetInt("DieNum", 0);
        dieNum++;
        PlayerPrefs.SetInt("DieNum", dieNum);
    }
    
    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 11, true);
        Physics2D.IgnoreLayerCollision(8, 3, true);
        inVulnerable = true;
        for(int i=0; i<numberOfFlashes; i++)
        {
            render.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            render.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 11, false);
        Physics2D.IgnoreLayerCollision(8, 3, false);
        inVulnerable = false;
    }
}
