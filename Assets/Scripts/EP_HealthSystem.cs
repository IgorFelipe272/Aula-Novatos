using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EP_HealthSystem : MonoBehaviour
{
    public int vida = 5;
    public float tempoInvencibilidade = 1.5f;

    private bool estaInvencivel = false;

    [Header("Referência para a UI de vida")]
    public EP_LifeUIController uiController;

    public void TakeDamage(int amount)
    {
        if (estaInvencivel)
            return;

        vida -= amount;
        Debug.Log("Tomou dano! Vida restante: " + vida);

        if(CompareTag("Player") && uiController != null)
        {
            uiController.AtualizarCoroes();
        }

        if(vida <= 0)
        {
            Debug.Log("Player morreu!");

            if(CompareTag("Player"))
            {
                Debug.Log("Player morreu. Aqui você pode chamar animação ou reiniciar a fase.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            StartCoroutine(Invencibilidade());
        }
    }

    private IEnumerator Invencibilidade()
    {
        estaInvencivel = true;
        yield return new WaitForSeconds(tempoInvencibilidade);
        estaInvencivel = false;
    }
}

