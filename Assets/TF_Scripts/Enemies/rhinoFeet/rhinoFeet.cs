using UnityEngine;

public class rhinoFeet : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    public float enemySpeed = 5f;
    public int startDirection = 1;
    private int currentDirection;
    private float halfWidth;
    private float halftHeight;
    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        halfWidth = sr.bounds.extents.x;
        halftHeight = sr.bounds.extents.y;
        currentDirection = startDirection;
    }


    void FixedUpdate()
    {
        movement.x = enemySpeed * currentDirection;
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
        setDirection();
    }

    private void setDirection()
    {
        Vector2 rightPos = transform.position;
        Vector2 leftPos = transform.position;
        rightPos.x += halftHeight;
        leftPos.x += halfWidth;

        if (rb.linearVelocity.x > 0)
        {
            if (Physics2D.Raycast(transform.position, Vector2.right, halfWidth + 0.1f, LayerMask.GetMask("groundLayer")))
            {
                currentDirection *= -1;
                flip();
            }
            else if (!Physics2D.Raycast(rightPos, Vector2.down, halftHeight + 0.1f, LayerMask.GetMask("groundLayer")))
            {
                currentDirection *= -1;
                flip();

            }
        }
        else if (rb.linearVelocity.x < 0)
        {
            if (Physics2D.Raycast(transform.position, Vector2.left, halfWidth + 0.1f, LayerMask.GetMask("groundLayer")))
            {
                currentDirection *= -1;
                flip();
            }
            else if (!Physics2D.Raycast(leftPos, Vector2.down, halftHeight + 0.1f, LayerMask.GetMask("groundLayer")))
            {
                currentDirection *= -1;
                flip();

            }
        }


        Debug.DrawRay(transform.position, Vector2.right * (halfWidth + 0.1f), Color.red);
        Debug.DrawRay(transform.position, Vector2.left * (halfWidth + 0.1f), Color.red);
        Debug.DrawRay(transform.position, Vector2.down * (halfWidth + 0.1f), Color.red);
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
