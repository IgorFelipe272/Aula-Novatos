using UnityEngine;

public class BF_EnemyFootPatrol : MonoBehaviour
{
    public Transform enemyTransform; // arraste o inimigo aqui no inspetor
    public Rigidbody2D enemyRb;      // arraste o Rigidbody2D do inimigo
    public float moveSpeed = 2f;
    public LayerMask groundLayer;

    private bool isGrounded = true;
    private Vector2 moveDirection = Vector2.left;

    private int numCollisions = 0;

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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidiu com um chão, verificar se deve mudar de direcao
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // Adiciona um no contador de colisões
            numCollisions = numCollisions + 1;
            if (numCollisions >= 2)
            {
                Flip();
            }
            //Debug.Log("enter " + numCollisions);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Se saiu do ch�o, inverter dire��o
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // Remove um no contador de colisões
            numCollisions = numCollisions - 1;

            // Se não colide com nada
            if(numCollisions == 0)
            {
                Flip();
            }
            //Debug.Log("exit " + numCollisions);
        }
    }

    private void Flip()
    {
        // Inverter dire��o de movimento
        moveDirection *= -1;

        // Flipar visualmente (espelhar) o inimigo
        Vector3 scale = enemyTransform.localScale;
        scale.x *= -1;
        enemyTransform.localScale = scale;
    }
}