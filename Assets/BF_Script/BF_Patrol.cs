using UnityEngine;

public class BF_Patrol : MonoBehaviour
{
    public Rigidbody2D enemyRb;    
    public float moveSpeed = 2f;

    public BoxCollider2D pe;        // Colisor do pé
    public BoxCollider2D frente;    // Colisor da frente

    private Vector2 moveDirection = Vector2.left;
    public Vector2 DirecaoMovimento => moveDirection;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Move o inimigo sempre na dire��o atual
        enemyRb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, enemyRb.linearVelocity.y);
    }

    public void Flip()
    {
        // Inverter dire��o de movimento
        moveDirection *= -1;

        // Flipar visualmente (espelhar) o inimigo
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}