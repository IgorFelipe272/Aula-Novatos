using UnityEngine;
using System.Collections;

public class AA_hit : MonoBehaviour
{
    public int dano = 1;
    public float knockbackForce = 5f;

    public float knockbackDelay = 1f;

    public AA_enemyFootPatrol patrol;

    private void Start()
    {

        if (patrol == null)
        {
            Debug.Log("AA_Patrol n�o foi atribuido");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Aplica dano
            IF_HealthSystem healthSystem = collision.GetComponent<IF_HealthSystem>();
            if (healthSystem != null)
            {
                healthSystem.TakeDamage(dano);
            }

            // Knockback baseado na dire��o do inimigo
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();
            if (playerRb != null && patrol != null)
            {
                Vector2 direcao = patrol.DirecaoMovimento.normalized;

                // Adiciona componente vertical pra criar a par�bola
                Vector2 knockbackDir = new Vector2(direcao.x, 1f).normalized;

                StartCoroutine(AplicarKnockbackComDelay(playerRb, knockbackDir, knockbackDelay));

            }
        }
    }


    private IEnumerator AplicarKnockbackComDelay(Rigidbody2D playerRb, Vector2 knockbackDir, float duracaoControleDesativado)
    {
        playerRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

        IF_MovPlataformer mov = playerRb.GetComponent<IF_MovPlataformer>();

        if (mov != null)
        {
            mov.enabled = false;  // desativa controle
            yield return new WaitForSeconds(duracaoControleDesativado);
            mov.enabled = true;  // ativa controle
        }
    }
}