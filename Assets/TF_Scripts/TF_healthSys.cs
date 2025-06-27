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

        vida -= dano;
        Debug.Log("Vida atual: " + vida);

        if (CompareTag("Player") && uiController != null)
        {
            uiController.AtualizarCores();
        }



        if (vida <= 0)
        {
            Debug.Log("Morreu");

            if (CompareTag("Player"))
            {
                Debug.Log("Game Over");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    void Start()
    {

    }


    void Update()
    {

    }
}
