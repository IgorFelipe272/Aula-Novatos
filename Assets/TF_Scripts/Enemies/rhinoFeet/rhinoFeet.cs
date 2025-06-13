using UnityEngine;

public class rhinoFeet : MonoBehaviour
{
    public float speed = 2f;
    public Transform groundCheck;   // Checa chão à frente
    public Transform wallCheck;     // Checa parede à frente
    public float checkDistance = 1f;
    public LayerMask groundLayer;

    private bool movingRight = true;

    void Update()
    {
        Patrol();
        Debug.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * checkDistance, Color.red);
        Debug.DrawLine(wallCheck.position, wallCheck.position + (movingRight ? Vector3.right : Vector3.left) * checkDistance, Color.blue);
    }

    void Patrol()
    {
        // Move o inimigo
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Raycast para detectar chão
        bool noGround = !Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

        // Raycast para detectar parede
        Vector2 wallDirection = movingRight ? Vector2.right : Vector2.left;
        bool wallDetected = Physics2D.Raycast(wallCheck.position, wallDirection, checkDistance, groundLayer);

        if (noGround || wallDetected)
        {
            Flip();
        }
    }

    void Flip()
    {
        // Inverte direção
        movingRight = !movingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        // Inverte o movimento
        speed *= -1;
    }

}
