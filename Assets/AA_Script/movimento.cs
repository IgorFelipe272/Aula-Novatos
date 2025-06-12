using UnityEngine;

public class movimento : MonoBehaviour
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
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y); // Corrigido: era "linearVelocity"


        // Flip do personagem
        
        if (moveInput != 0)
         {
            Flip(moveInput);
        }
        
        //para o jogador andar 
        if (moveInput != 0) {
           animator.SetBool("Run", true);
        }
        else
            animator.SetBool("Run", false);
        // Verifica se esta no chao
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Resetar pulos ao tocar o ch�o
        if (isGrounded)
        {
            pulosRestantes = quantidadeDePulosExtra;
        }

        // Pular
        if (Input.GetButtonDown("Jump") && pulosRestantes > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            pulosRestantes--;
            animator.SetTrigger("Jump"); // Ativa anima��o de pulo
        }

        // Atualizar par�metros do Animator
        animator.SetFloat("Velocidade", Mathf.Abs(moveInput));
        animator.SetBool("NoChao", isGrounded);
        animator.SetFloat("VelY", rb.linearVelocity.y); // Para saber se est� subindo ou caindo
    }
    
    void Flip(float direcao)
    {
        transform.localScale =  new Vector3(Mathf.Sign(direcao),1f, 1f);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}













