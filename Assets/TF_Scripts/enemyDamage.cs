using UnityEngine;

public class enemyDamage : MonoBehaviour
{
    public int dano = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            TF_healthSys health = collision.gameObject.GetComponent<TF_healthSys>();
            if (health != null)
            {
                health.takeDamage(dano);
                health.uiController.AtualizarCores();
            }
        }
    }
}
