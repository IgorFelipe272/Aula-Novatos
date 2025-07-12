using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class playerMovement : MonoBehaviour
{
    [Header("Movimentacao")]
    public float speed = 5f;
    private float moveInput;

    [Header("Pulo")]
    public float jumpForce = 10f;
    public int quantidadeDePulosExtra = 1;
    private int pulosRestantes;

    [Header("Coyote Time")]
    public float coyoteTime = 0.15f;
    private float coyoteTimeCounter;

    [Header("Chao")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    private bool isDashing;
    private bool canDash = true;
    private Vector2 dashDi;
    private TrailRenderer trailRenderer;
    private List<Collider2D> ignoredColliders = new List<Collider2D>();

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D playerCollider;
    [HideInInspector]
    public bool canMove = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pulosRestantes = quantidadeDePulosExtra;
        trailRenderer = GetComponent<TrailRenderer>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (!canMove) return;
        // horizontal
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // dash
        if (Input.GetButtonDown("Dash") && canDash)
        {
            isDashing = true;
            canDash = false;
            dashDi = new Vector2(moveInput, Input.GetAxisRaw("Vertical")).normalized;

            if (dashDi == Vector2.zero)
            {
                dashDi = new Vector2(Mathf.Sign(moveInput), 0);
            }

            rb.linearVelocity = dashDi * dashSpeed;
            trailRenderer.emitting = true;

            IgnoreColTag("Enemy");

            StartCoroutine(StopDash());
        }

        if (isDashing)
        {
            rb.linearVelocity = dashDi.normalized * dashSpeed;
            return;
        }


        // flip
        if (moveInput != 0)
        {
            Flip(moveInput);
        }

        // esta no chao?
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Coyote time logic
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            pulosRestantes = quantidadeDePulosExtra;
            canDash = true;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Pular (Jump)
        if (Input.GetButtonDown("Jump") && (coyoteTimeCounter > 0f || pulosRestantes > 0))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            pulosRestantes--;
            coyoteTimeCounter = 0f;
            animator.SetBool("isJumping", !isGrounded);
        }

        // Atualizar parâmetros do Animator
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetBool("isJumping", !isGrounded);
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    void Flip(float direcao)
    {
        transform.localScale = new Vector3(Mathf.Sign(direcao), 1f, 1f);
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        trailRenderer.emitting = false;
        rb.linearVelocity = new Vector2(2f, 2f);
        ReactCol();
    }
    public bool IsDashing()
    {
        return isDashing;
    }
    void IgnoreColTag(string tag)
    {
        GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject obj in objetos)
        {
            Collider2D otherCollider = obj.GetComponent<Collider2D>();
            if (otherCollider != null)
            {
                Physics2D.IgnoreCollision(playerCollider, otherCollider, true);
                ignoredColliders.Add(otherCollider);
            }
        }
    }

    void ReactCol()
    {
        foreach (Collider2D col in ignoredColliders)
        {
            if (col != null)
            {
                Physics2D.IgnoreCollision(playerCollider, col, false);
            }
        }

        ignoredColliders.Clear();
    }

}
