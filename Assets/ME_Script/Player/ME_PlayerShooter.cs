using UnityEngine;

public class ME_PlayerShooter : MonoBehaviour
{
    [Header("Referências")]
    public GameObject tiroPrefab;       // Prefab do tiro
    public Transform pontoDisparo;      // Local de onde o tiro sai 

    [Header("Configuração")]
    public float velocidadeTiro = 10f;      // Velocidade do projétil
    public float tempoEntreTiros = 0.3f;    // Tempo entre cada tiro 

    private float tempoProximoTiro = 0f;    // Controle interno de tempo

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= tempoProximoTiro)
        {
            Atirar();
            tempoProximoTiro = Time.time + tempoEntreTiros;
        }
    }

    void Atirar()
    {
        GameObject tiro = Instantiate(tiroPrefab, pontoDisparo.position, Quaternion.identity);

        Rigidbody2D rb = tiro.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Direção com base na escala do personagem
            float direcao = Mathf.Sign(transform.localScale.x);

            rb.linearVelocity = new Vector2(direcao * velocidadeTiro, 0f);

            // Espelha o tiro se o personagem estiver virado para a esquerda
            Vector3 escalaTiro = tiro.transform.localScale;
            escalaTiro.x = Mathf.Abs(escalaTiro.x) * direcao;
            tiro.transform.localScale = escalaTiro;
        }
    }
}
