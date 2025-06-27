using UnityEngine;
using System.Collections;

public class TF_hit : MonoBehaviour
{
    public int dano = 1;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.5f;
    public rhinoFeet patrol;
    void Start()
    {
        if (patrol == null)
        {
            Debug.LogError("Patrol script not assigned in TF_hit.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TF_healthSys healthSys = collision.GetComponent<TF_healthSys>();
        if (healthSys != null)
        {
            healthSys.takeDamage(dano);
        }

        Rigidbody2D playerrb = collision.GetComponent<Rigidbody2D>();
        if(playerrb != null && patrol != null)
        {
            // Aplica knockback
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            playerrb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

            // Inverte a direção do patrulheiro
            patrol.Flip();

            // Inicia o tempo de knockback
            StartCoroutine(ApplyKnockback(playerrb, knockbackDirection, 0.5f));
        }
    }
    private IEnumerator ApplyKnockback(Rigidbody2D playerRb, Vector2 knockbackDirection, float desableColliderTime)
    {
        playerRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        playerRb.GetComponent<Collider2D>().enabled = false; // Desativa o collider

        yield return new WaitForSeconds(knockbackDuration);

        playerRb.linearVelocity = Vector2.zero; // Reseta a velocidade
        playerRb.GetComponent<Collider2D>().enabled = true; // Reativa o collider
    }
   
}
