using UnityEngine;

public class EP_PlayerShooter : MonoBehaviour
{
    [Header("Referências")]
    public GameObject tiroPrefab;
    public Transform pontoDisparo;

    [Header("Configuração")]
    public float velocidadeTiro = 10f;

    void Update()
    {
        if (Input.GetMouseButton(0))
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
            float direcao = Mathf.Sign(transform.localScale.x);
            rb.linearVelocity = new Vector2(direcao = velocidadeTiro, 0f);

            Vector3 escalaTiro = tiro.transform.localScale;
            escalaTiro.x *= direcao;
            tiro.transform.localScale = escalaTiro;
        }
    }
}
