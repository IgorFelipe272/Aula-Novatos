using UnityEngine;

public class JB_Bala : MonoBehaviour
{
    
    public int dano = 1;
    public float tempoDestruicao = 3f;

    void Start()
    {
        //Destroi a bala automaticamente após alguns segundos (caso não colida com nada)
        Destroy(gameObject, tempoDestruicao);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Garante que a balanão interaja com o jogador
        if(!collision.CompareTag("Player"))
        {
            JB_HealthSystem alvo = collision.GetComponent<JB_HealthSystem>();
            if(alvo != null)
            {
                alvo.TakeDamage(dano);
            }

            //Destroi a bala apenas se bater em algo que não seja o jogador
            Destroy(gameObject);
        }
    }
}
