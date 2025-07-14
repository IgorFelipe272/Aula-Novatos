using UnityEngine;

public class ME_MovPlataformer : MonoBehaviour
{
    [Header("Movimentacao")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Pulo Multiplo")]
    public int quantidadeDePulosExtra = 2;
    private int pulosRestantes;

    [Header("Verificacao de chao")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody2D rb;
    private float moveInput;

    private Animator animator;

    [Header("Efeito de Pulo Duplo")]
    public GameObject efeitoPulo;

    [Header("Local onde o efeito de pulo vai aparecer")]
    public Transform pontoEfeitoPulo;

    [Header("wall jump")]
    public bool NaParede;
    public Vector3 wallOfset;
    public float wallRadius;
    public float MaxFallSpeed = -1;
    public LayerMask wallLayer;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pulosRestantes = quantidadeDePulosExtra;
    }

    void Update()
    {
        // Entrada horizontal
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Resetar o contador de coyote se tocar no chao
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // decrementar coyote
        }

        // Flip do personagem
        if (moveInput != 0)
        {
            Flip(moveInput);
        }

        // Verifica se esta no chao
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Resetar pulos ao tocar o chão
        if (isGrounded)
        {
            pulosRestantes = quantidadeDePulosExtra;
        }

        // Pular
        if (Input.GetButtonDown("Jump"))
        {
            bool podePular = false;

            // Pulo do chão
            if (coyoteTimeCounter > 0f)
            {
                podePular = true;
            }

            // Pulo na parede (não gasta pulo extra)
            else if (NaParede)
            {
                podePular = true;
            }

            // Pulo extra
            else if (pulosRestantes > 0)
            {
                podePular = true;
                pulosRestantes--; // só gasta se não estiver na parede
            }

            if (podePular)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetTrigger("Jump");


                if (!isGrounded && !NaParede && efeitoPulo != null)
                {
                    GameObject efeito = Instantiate(efeitoPulo, pontoEfeitoPulo.position, Quaternion.identity);
                    Destroy(efeito, 0.3f);

                }
                coyoteTimeCounter = 0f; // Reseta o contador de Coyote após pular
            }
        }

        // Atualizar parâmetros do Animator
        animator.SetFloat("Velocidade", Mathf.Abs(moveInput));
        animator.SetBool("NoChao", isGrounded);
        animator.SetFloat("VelY", rb.linearVelocity.y);
    }

    void Flip(float direcao)
    {
        transform.localScale = new Vector3(Mathf.Sign(direcao), 1f, 1f);
    }

    void DesativarEfeito()
    {
        if (efeitoPulo != null)
            efeitoPulo.SetActive(false);
    }

    void FixedUpdate()
    {
        physicsCheck(); // chamada do método que detecta a parede
    }

    void physicsCheck()
    {
        NaParede = false;

        bool rightwall = Physics2D.OverlapCircle(transform.position + new Vector3(wallOfset.x, 0), wallRadius, wallLayer);
        bool leftwall = Physics2D.OverlapCircle(transform.position + new Vector3(-wallOfset.x, 0), wallRadius, wallLayer);

        if (rightwall || leftwall)
        {
            NaParede = true;
        }
        if (NaParede)
        {
            if(rb.linearVelocity.y < MaxFallSpeed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, MaxFallSpeed);
            }
        }

        animator.SetBool("NaParede", NaParede); 
    }

    private void OnDrawGizmosSelected()
    {
        if (!Camera.current) return;

        Vector3 screenPos = Camera.current.WorldToViewportPoint(transform.position);
        if (screenPos.z < 0 || screenPos.x < 0 || screenPos.x > 1 || screenPos.y < 0 || screenPos.y > 1)
            return;

        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(wallOfset.x, 0), wallRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-wallOfset.x, 0), wallRadius);
    }

}
