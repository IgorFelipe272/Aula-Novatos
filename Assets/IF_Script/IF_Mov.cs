using UnityEngine;

public class IF_Mov : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(rb == null)
        {
            Debug.Log("N tem Rigidbody2D");
        }
    }

    

    void Update()
    {
        // Coleta entrada do jogador
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized; // Mantém velocidade constante ao andar na diagonal
    }


    private void FixedUpdate()
    {
        // Movimento do jogador
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

    }

}
