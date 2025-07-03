using UnityEngine;

public class AA_playerShooter : MonoBehaviour
{
    [Header("Refer�ncias")]
    public GameObject tiroPrefab;       // Prefab do tiro
    public Transform pontoDisparo;      // Local de onde o tiro sai

    [Header("Configura��o")]
    public float velocidadeTiro = 10f;  // Velocidade do proj�til

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Bot�o esquerdo do mouse
        {
            Atirar();
        }
    }

    void Atirar()
    {
        GameObject tiro = Instantiate(tiroPrefab, pontoDisparo.position, Quaternion.identity);

        Rigidbody2D rb = tiro.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Define a dire��o com base na escala do jogador (1 = direita, -1 = esquerda)
            float direcao = Mathf.Sign(transform.localScale.x);
            rb.linearVelocity = new Vector2(direcao * velocidadeTiro, 0f);

            // Espelha o tiro horizontalmente se o jogador estiver virado para a esquerda (opcional)
            Vector3 escalaTiro = tiro.transform.localScale;
            escalaTiro.x *= direcao;
            tiro.transform.localScale = escalaTiro;
        }
    }
}