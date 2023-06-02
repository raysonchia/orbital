using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFeedback : MonoBehaviour
{

    [SerializeField]
    private float enemyFlashTime = 0.03f, flashCooldown = 0.06f;
    //private bool flashBreak; // cooldown unused for now
    [SerializeField]
    private Material flashMaterial;
    private SpriteRenderer targetSprite;
    private Material defaultMat;


    private void Start()
    {
        targetSprite = GetComponent<SpriteRenderer>();
        defaultMat = targetSprite.material;
    }

    public void PlayerHitEffect()
    {
        StartCoroutine(Damaged());
    }

    private IEnumerator Damaged()
    {
        // total animation duration equivalent to invulnerable time which is 1.2s
        yield return StartCoroutine(Flash());

        for (int i = 0; i < 5; i++)
        {
            yield return StartCoroutine(Wait());
            yield return StartCoroutine(Blink());
        }
    }

    private IEnumerator Flash(float time = 0.2f)
    {
        targetSprite.material = flashMaterial;
        yield return new WaitForSeconds(time);
        targetSprite.material = defaultMat;
    }

    private IEnumerator Blink()
    {
        targetSprite.enabled = false;
        yield return new WaitForSeconds(0.1f);
        targetSprite.enabled = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);
    }

    public void EnemyHitEffect()
    {
        StartCoroutine(Flash(enemyFlashTime));
    }
}
