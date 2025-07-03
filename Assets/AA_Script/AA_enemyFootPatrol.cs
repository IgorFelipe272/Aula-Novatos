using UnityEngine;

public class EnemyFootPatrol : MonoBehaviour
{
    public Transform enemyTransform; // arraste o inimigo aqui no inspetor
    public Rigidbody2D enemyRb;      // arraste o Rigidbody2D do inimigo
    public float moveSpeed = 2f;
    public LayerMask groundLayer;

    public LayerMask wallLayer;

    private bool isGrounded = true;
    private Vector2 moveDirection = Vector2.right;

    private void Start()
    {
        if (enemyTransform == null)
            enemyTransform = transform.parent;

        if (enemyRb == null)
            enemyRb = enemyTransform.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Move o inimigo sempre na dire��o atual
        enemyRb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, enemyRb.linearVelocity.y);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Se saiu do ch�o, inverter dire��o
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
             Flip();
        }
    }

    private void Flip()
    {
        // Inverter dire��o de movimento
        moveDirection = -moveDirection;

        // Flipar visualmente (espelhar) o inimigo
        Vector3 scale = enemyTransform.localScale;
        scale.x *= -1;
        enemyTransform.localScale = scale;
    }
}