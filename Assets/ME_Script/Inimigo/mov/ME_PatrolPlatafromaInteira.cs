using UnityEngine;

public class ME_PatrolPlatafromaInteira : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float delayGiro = 0.1f;

    private Rigidbody2D enemyRb;
    private Vector2 moveDirection = Vector2.right;
    private float tempoUltimoGiro = 0f;
    private bool encostandoNaPlataforma = true;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        moveDirection = transform.right.normalized;
    }

    private void FixedUpdate()
    {
        enemyRb.linearVelocity = moveDirection * moveSpeed;

        if (!encostandoNaPlataforma && Time.time - tempoUltimoGiro > delayGiro)
        {
            transform.Rotate(0f, 0f, -90f);
            moveDirection = transform.right.normalized;
            tempoUltimoGiro = Time.time;
            encostandoNaPlataforma = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Plataforma"))
        {
            encostandoNaPlataforma = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plataforma"))
        {
            encostandoNaPlataforma = false;
        }
    }
}
