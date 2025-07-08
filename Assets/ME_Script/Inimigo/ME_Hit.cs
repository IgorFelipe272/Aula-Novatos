using UnityEngine;
using System.Collections;

public class ME_Hit : MonoBehaviour
{
    public int dano = 1;
    public float knockbackForce = 5f;
    public float knockbackDelay = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aplica dano
            ME_HealthSystem healthSystem = collision.GetComponent<ME_HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(dano);
            }

            // Aplica knockback baseado na posição
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Direção do knockback com base na posição relativa
                Vector2 direcao = (collision.transform.position - transform.position).normalized;

                // Adiciona força pra cima pra dar aquele "pulo" pra trás
                Vector2 knockbackDir = new Vector2(direcao.x, 1f).normalized;

                StartCoroutine(AplicarKnockbackComDelay(playerRb, knockbackDir, knockbackDelay));
            }
        }
    }

    private IEnumerator AplicarKnockbackComDelay(Rigidbody2D playerRb, Vector2 knockbackDir, float duracaoControleDesativado)
    {
        playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        ME_MovPlataformer mov = playerRb.GetComponent<ME_MovPlataformer>();

        if (mov != null)
        {
            mov.enabled = false;  // desativa controle por um tempo
            yield return new WaitForSeconds(duracaoControleDesativado);
            mov.enabled = true;   // ativa controle novamente
        }
    }
}
