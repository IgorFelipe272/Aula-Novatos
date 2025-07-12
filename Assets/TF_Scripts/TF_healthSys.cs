using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TF_healthSys : MonoBehaviour
{
    public int vida = 5;
    protected int maxVida = 0;
    public float tempoInvencivel = 2f;
    protected bool invencivel = false;
    public playerMovement movimento;
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.3f;
    private Rigidbody2D rb;
    private Collider2D playerCollider;

    [Header("Referencia para UI de vida")]
    public TF_Lifebar uiController;
    private List<Collider2D> ignoredColliders = new List<Collider2D>();
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movimento = GetComponent<playerMovement>();
        playerCollider = GetComponent<Collider2D>();
        maxVida = vida;
    }

    public virtual void takeDamage(int dano, Vector2 sourcePosition, Collider2D enemyCollider)
    {


        if (invencivel || (movimento != null && movimento.IsDashing()))
            return;

        vida -= dano;

        if (CompareTag("Player") && uiController != null)
        {
            uiController.AtualizarCores();
            
        }


        if (vida <= 0)
        {


            if (CompareTag("Player"))
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("Morreu");
            }
            else
            {

                Destroy(gameObject);
            }
        }
        else
        {
            StartCoroutine(Invencible());
            if (CompareTag("Player"))
            {
                StartCoroutine(ApplyKnockback(sourcePosition, enemyCollider));
            }
        }
        Debug.Log("Tomando dano! EnemyCollider: " + (enemyCollider != null));
    }


    public IEnumerator Invencible()
    {
        invencivel = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float elapsedTime = 0f;
        while (elapsedTime < tempoInvencivel)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            elapsedTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.enabled = true;
        invencivel = false;
    }
    public IEnumerator ApplyKnockback(Vector2 sourcePosition, Collider2D enemyCollider)
    {
        if (movimento != null)
            movimento.canMove = false;

        if (enemyCollider != null && playerCollider != null)
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, true);

        Vector2 knockDirection = (rb.position - sourcePosition).normalized;
        rb.linearVelocity = Vector2.zero; // Clear current velocity
        rb.AddForce(knockDirection * knockbackForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        // Re-enable movement
        if (movimento != null)
            movimento.canMove = true;

        // Re-enable collision
        if (enemyCollider != null && playerCollider != null)
            Physics2D.IgnoreCollision(playerCollider, enemyCollider, false);
        Debug.Log("Aplicando knockback");
    }
    
}

    