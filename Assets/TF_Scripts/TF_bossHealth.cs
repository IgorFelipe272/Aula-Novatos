using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TF_bossHealth : TF_healthSys
{
    public bosshealthbar hBB;
    public AudioClip deathSound;
    public GameObject gameOverPrefab;

    override public void takeDamage(int dano, Vector2 sourcePosition, Collider2D enemyCollider)
    {

        if (invencivel)
            return;

        vida -= dano;

        hBB.UpdateHealthBarBoss((float)vida, (float)maxVida);
        Debug.Log("Vida atual: " + vida);

        if (vida <= 0)
        {
            KILLYOURSELFNOW();
        }
        else
        {
            StartCoroutine(Invencible());
        }
    }
    void KILLYOURSELFNOW()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 10f);

        Debug.Log("Boss morreu");

        Instantiate(gameOverPrefab, Vector3.zero, Quaternion.identity);

        Destroy(gameObject);
        
        
    }

}
