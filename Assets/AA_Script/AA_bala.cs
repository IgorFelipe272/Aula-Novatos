using UnityEngine;

public class AA_bala : MonoBehaviour
{
    public int dano = 1;
    public float tempoDestruicao = 3f;

    void Start()
    {
        // Destroi a bala automaticamente ap�s alguns segundos (caso n�o colida com nada)
        Destroy(gameObject, tempoDestruicao);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Garante que a bala n�o interaja com o jogador
        if (!collision.CompareTag("Player"))
        {
            IF_HealthSystem alvo = collision.GetComponent<IF_HealthSystem>();
            if (alvo != null)
            {
                alvo.TakeDamage(dano);
            }

            // Destroi a bala apenas se bater em algo que n�o seja o jogador
            Destroy(gameObject);
        }
    }
}