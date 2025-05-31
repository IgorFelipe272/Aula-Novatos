using UnityEngine;

public class movimentacao : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody2D rb;
    private Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
            {
            Debug.Log("nao tem Rigidbody2D");
            }
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        moveInput.x = moveInput.normalized;
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

}













