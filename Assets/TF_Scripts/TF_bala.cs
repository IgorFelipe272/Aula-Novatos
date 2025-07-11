using UnityEngine;

public class TF_bala : MonoBehaviour
{

    public int dano = 1;
    public float tempoDeVida = 5f;
    void Start()
    {

        Destroy(gameObject, tempoDeVida);  
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.gameObject.CompareTag("Player"))
        {
            TF_healthSys alvo = collision.gameObject.GetComponent<TF_healthSys>();
            if (alvo != null)
            {
                alvo.takeDamage(dano);
            }
        }
            Destroy(gameObject);
    }
    
        
    
}
