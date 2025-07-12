using UnityEngine;

public class bossBalaAttack : MonoBehaviour
{

    public int dano = 1;
    public float tempoDeVida = 5f;
    public LayerMask layersAfetadas;

    void Start()
    {

        Destroy(gameObject, tempoDeVida);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Boss"))
            return;

        
        if (((1 << collision.gameObject.layer) & layersAfetadas) != 0)
        {
            
            TF_healthSys alvo = collision.GetComponent<TF_healthSys>();
            if (alvo != null)
            {
                alvo.takeDamage(dano, transform.position, null);
            }

            Destroy(gameObject);
        }
    }

}
