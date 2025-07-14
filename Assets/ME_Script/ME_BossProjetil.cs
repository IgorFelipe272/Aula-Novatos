using UnityEngine;

public class ME_BossProjetil : MonoBehaviour
{
    public float velocidade = 5f;
    public int dano = 1;
    public Vector2 direcao = Vector2.left;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direcao.normalized * velocidade;

        Debug.Log("Velocidade real aplicada: " + rb.linearVelocity);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ME_HealthSystem player = collision.GetComponent<ME_HealthSystem>();
            if (player != null)
            {
                player.TakeDamage(dano);
            }
        }

        Destroy(gameObject);
    }
}
