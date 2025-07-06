using UnityEngine;
using System.Collections;

public class EP_Hit : MonoBehaviour
{
    public int dano = 1;
    public float knockbackForce = 5f;

    public float knockbackDelay = 1f;

    public EP_Patrol patrol;

    private void Start()
    {
        if (patrol == null)
        {
            Debug.Log("EP_Patrol não foi atribuido.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EP_HealthSystem healthSystem = collision.GetComponent<EP_HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(dano);
            }

            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null && patrol != null)
            {
                Vector2 direcao = patrol.DirecaoMovimento.normalized;

                Vector2 knockbackDir = new Vector2(direcao.x, 1f).normalized;

                StartCoroutine(AplicarKnockbackComDelay(playerRb, knockbackDir, knockbackDelay));
            }

        }
    }

    private IEnumerator AplicarKnockbackComDelay(Rigidbody2D playerRb, Vector2 knockbackDir, float duracaoControleDesativado)
    {
        playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        EP_Mov2 mov = playerRb.GetComponent<EP_Mov2>();

        if (mov != null)
        {
            mov.enabled = false;
            yield return new WaitForSeconds(duracaoControleDesativado);
            mov.enabled = true;
        }
    }
}
