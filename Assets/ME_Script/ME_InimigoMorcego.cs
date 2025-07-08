using UnityEngine;

public class ME_InimigoMorcego : MonoBehaviour
{
    enum Estado { Patrulha, Aguardando, DandoDash }
    Estado estadoAtual = Estado.Patrulha;

    [Header("Configurações")]
    public float velocidade = 2f;
    public float distanciaMaxima = 5f;
    public float tempoEsperaAntesDash = 1f;
    public float forcaDash = 10f;
    public float duracaoDash = 0.5f;
    public Transform jogador;

    private Rigidbody2D rb;
    private Vector2 posicaoInicial;
    private int direcao = 1;
    private float timerEspera = 0f;
    private bool jogadorDetectado = false;
    private Vector2 posicaoDoDash;
    private float timerDash = 0f;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        posicaoInicial = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (estadoAtual)
        {
            case Estado.Patrulha:
                Patrulhar();
                break;
            case Estado.Aguardando:
                AguardarAntesDoDash();
                break;
            case Estado.DandoDash:
                ExecutarDash();
                break;
        }

        if (rb.linearVelocity.x > 0.1f) spriteRenderer.flipX = false;
        else if (rb.linearVelocity.x < -0.1f) spriteRenderer.flipX = true;
    }

    void Patrulhar()
    {
        rb.linearVelocity = new Vector2(velocidade * direcao, 0f);

        float distanciaAtual = transform.position.x - posicaoInicial.x;

        if (direcao == 1 && distanciaAtual >= distanciaMaxima)
            direcao = -1;
        else if (direcao == -1 && distanciaAtual <= -distanciaMaxima)
            direcao = 1;

        if (jogadorDetectado)
        {
            rb.linearVelocity = Vector2.zero;
            timerEspera = tempoEsperaAntesDash;
            posicaoDoDash = jogador.position;  // Salva posição do jogador
            estadoAtual = Estado.Aguardando;
        }
    }

    void AguardarAntesDoDash()
    {
        timerEspera -= Time.deltaTime;

        if (timerEspera <= 0)
        {
            // Começa o dash
            timerDash = duracaoDash;
            estadoAtual = Estado.DandoDash;

            Vector2 direcaoDash = (posicaoDoDash - (Vector2)transform.position).normalized;
            rb.linearVelocity = direcaoDash * forcaDash;
        }
    }

    void ExecutarDash()
    {
        timerDash -= Time.deltaTime;
        if (timerDash <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            estadoAtual = Estado.Patrulha;
            jogadorDetectado = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorDetectado = true;
        }
    }
}
