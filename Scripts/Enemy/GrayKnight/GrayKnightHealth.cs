using UnityEngine;
using System.Collections;

public class GrayKnightHealth : EnemyHealth
{
    //　グレーナイトはパーツごとにSpriteRendererがあるため、ダメージを喰らったときひとつずつ色を変える必要があります
    private SpriteRenderer[] renders;
    public override void Awake()
    {
        base.Awake();
        originalMaterial = GetComponentInChildren<SpriteRenderer>().material;
        renders = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) {
            TurnWhite();
        }

    }
    public override IEnumerator Invulnerability()
    {
        inVulnerable = true;
        for(int i=0; i<numberOfFlashes; i++)
        {
            TurnWhite();
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            TurnColorBack();
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        inVulnerable = false;
    }

    private void TurnWhite()
    {
        foreach (SpriteRenderer r in renders) {
            r.color = Color.white;
            r.material = flashMaterial;
        }
    }

    private void TurnColorBack()
    {
        foreach (SpriteRenderer r in renders)
        {
            r.material = originalMaterial;
            if (r.name != "Sword" && r.name != "Helmet")
                r.color = Color.gray;
        }
    }
}
