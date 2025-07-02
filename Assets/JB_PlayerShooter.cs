using UnityEngine;

public class JB_PlayerShooter : MonoBehaviour
{
    
    [Header("Referências")]
    public GameObject tiroPrefab;
    public Transform pontoDisparo;

    [Header("Configuração")]
    public float velocidadeTiro = 10f;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))// Botão esquerdo do mouse
        {
            Atirar();
        }
    }

    void Atirar()
    {
        GameObject tiro = Instantiate(tiroPrefab, pontoDisparo.position, Quaternion.identity);

        Rigidbody2D rb = tiro.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            //Define a direção com base na escala do jogador(1 = direita, -1 = esquerda)
            float direcao = Mathf.Sign(transform.localScale.x);
            rb.linearVelocity = new Vector2(direcao * velocidadeTiro, 0f);

            //Espelha o tiro horizontalmente se o jogador estiver virado para a esquerda (opcional)
            Vector3 escalaTiro = tiro.transform.localScale;
            escalaTiro.x *= direcao;
            tiro.transform.localScale = escalaTiro;
        }
    }
}
