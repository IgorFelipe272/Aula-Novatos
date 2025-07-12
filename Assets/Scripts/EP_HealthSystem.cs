using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EP_HealthSystem : MonoBehaviour
{
    public int vidaMax = 5;
    public int vidaAtual = 5;
    public float tempoInvencibilidade = 1.5f;
    static public int inimigo = 3;

    private bool estaInvencivel = false;

    [Header("Referência para a UI de vida")]
    public EP_LifeUIController uiController;

    public void TakeDamage(int amount)
    {
        if (estaInvencivel)
            return;

        vidaAtual -= amount;
        Debug.Log("Tomou dano! Vida restante: " + vidaAtual);

        if(CompareTag("Player") && uiController != null)
        {
            uiController.AtualizarCoroes();
        }

        if(vidaAtual <= 0)
        {
            Debug.Log("Player morreu!");

            if(CompareTag("Player"))
            {
                Debug.Log("Player morreu. Aqui você pode chamar animação ou reiniciar a fase.");
                SceneManager.LoadScene("Death_Scene");
            }
            else
            {
                Destroy(gameObject);
                inimigo -= 1;
            }
            if(inimigo <= 0)
            {
                SceneManager.LoadScene("Win_Scene");
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

