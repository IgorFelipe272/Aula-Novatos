using UnityEngine;

public class GE_EnemyFootPatrol : MonoBehaviour
{
    public Transform enemyTransform;
    public Rigidbody2D enemyRb;
    public float moveSpeed = 2f;
    public LayerMask groundLayer;
    public LayerMask wallLayer;

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
        // Move o inimigo na direção atual
        enemyRb.velocity = new Vector2(moveDirection.x * moveSpeed, enemyRb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidiu com parede, dar flip
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            Flip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Se saiu do chão, dar flip
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        // Inverter direção
        moveDirection *= -1;

        // Inverter visualmente
        Vector3 scale = enemyTransform.localScale;
        scale.x *= -1;
        enemyTransform.localScale = scale;
    }
}
