using UnityEngine;

public class EP_Patrol : MonoBehaviour
{
    public Rigidbody2D enemyRb;      // arraste o Rigidbody2D do inimigo
    public float moveSpeed = 2f;

    public BoxCollider2D pe;
    public BoxCollider2D frente;

    private Vector2 moveDirection = Vector2.right;

    public Vector2 DirecaoMovimento => moveDirection;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        enemyRb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, enemyRb.linearVelocity.y);
    }

    public void Flip()
    {
        moveDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
