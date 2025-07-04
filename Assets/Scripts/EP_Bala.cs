using UnityEngine;

public class EP_Bala : MonoBehaviour
{
    public int dano = 1;
    public float tempoDestruicao = 3f;

    void Start()
    {
        Destroy(gameObject, tempoDestruicao);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            EP_HealthSystem alvo = collision.GetComponent<EP_HealthSystem>();
            if(alvo != null)
            {
                alvo.TakeDamage(dano);
            }

            Destroy(gameObject);
        }
    }
}
