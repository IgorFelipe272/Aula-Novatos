using UnityEngine;

public class JM_Rhino : MonoBehaviour
{
    public float velocidade = 2f;
    private Rigidbody2D rb;
    private int direcao = 1; // 1 = direita, -1 = esquerda

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PontodeVirada>()!=null)
        {
            InverterDirecao();
        }
    }

    void InverterDirecao()
    {
        direcao *= -1;
        Flip();
    }

    void Flip()
    {
        transform.localScale = new Vector3(Mathf.Sign(direcao), 1f, 1f);
    }
}