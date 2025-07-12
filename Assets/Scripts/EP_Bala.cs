using UnityEngine;
using UnityEngine.Rendering;

public class EP_Bala : MonoBehaviour
{
    public int dano = 1;
    public float tempoDestruicao = 3f;
    private EP_HealthSystem healthSystem;

    void Start()
    {
        Destroy(gameObject, tempoDestruicao);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")&&(!collision.CompareTag("Itens")))
        {
            EP_HealthSystem alvo = collision.GetComponent<EP_HealthSystem>();
            if(alvo != null)
            {
                alvo.TakeDamage(dano);
            }

            Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            EP_HealthSystem player = collision.GetComponent<EP_HealthSystem>();
            if(player != null)
            {
                player.TakeDamage(dano);
                StartCoroutine(healthSystem.Invencibilidade());
            }

            Destroy(gameObject);
        }
    }
}
