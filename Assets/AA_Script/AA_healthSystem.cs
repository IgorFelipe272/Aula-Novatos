using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class AA_healthSystem : MonoBehaviour
{
    public int vida = 5;
    public float tempoInvencibilidade = 1.5f;

    private bool estaInvencivel = false;

    [Header("Refer�ncia para a UI de vida")]
    public AA_lifeUIController uiController;

    public void TakeDamage(int amount)
    {
        if (estaInvencivel)
            return;

        vida -= amount;
        Debug.Log("Tomou dano! Vida restante: " + vida);

        // Atualiza a UI aqui se o objeto for o jogador
        if (CompareTag("Player") && uiController != null)
        {
            uiController.AtualizarCoroes();
        }


        if (vida <= 0)
        {
            Debug.Log("Player morreu!");
            // L�gica de morte aqui

            if (CompareTag("Player"))
            {
                // L�gica especial para o jogador
                // Exemplo: desabilitar controle, tocar anima��o, recarregar cena, etc.
                Debug.Log("Player morreu! Aqui voc� pode chamar anima��o ou reiniciar a fase.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                // Se n�o for o jogador, destr�i o objeto
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
        // Aqui voc� pode adicionar um efeito visual, tipo piscar o sprite
        yield return new WaitForSeconds(tempoInvencibilidade);
        estaInvencivel = false;
    }

    /*
    private IEnumerator Invencibilidade()
    {
        estaInvencivel = true;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float elapsed = 0f;
        while (elapsed < tempoInvencibilidade)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        sr.enabled = true;
        estaInvencivel = false;
    }
    */
}