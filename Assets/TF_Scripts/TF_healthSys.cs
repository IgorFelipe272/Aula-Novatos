using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TF_healthSys : MonoBehaviour
{
    public int vida = 5;
    public float tempoInvencivel = 2f;
    private bool invencivel = false;

   [Header("Referencia para UI de vida")]
   public TF_Lifebar uiController;

    public void takeDamage(int dano)
    {
        if (invencivel) return;
        

        if (CompareTag("Player") && uiController != null)
        {
            uiController.AtualizarCores();
            
            vida -= dano;
            Debug.Log("Vida atual: " + vida);
        }



        if (vida <= 0)
        {


            if (CompareTag("Player"))
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("Morreu");
            }
            else
            {

                Destroy(gameObject);
            }
        }
        else
        {
            StartCoroutine(Invencible());
        }
    }


    private IEnumerator Invencible()
    {
        invencivel = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float elapsedTime = 0f;
        while (elapsedTime < tempoInvencivel)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            elapsedTime += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.enabled = true;
        invencivel = false;
    }
}
