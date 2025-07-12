using UnityEngine;
using UnityEngine.SceneManagement;

public class gemaNaoPlys : MonoBehaviour
{
    public string nomeDaFaseDestino = "boss";
    private float floatingTimer = 0f;
    public float floatingSpeed = 2f; // Velocidade de flutuação
    private Vector3 initialPosition;
    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        SceneManager.LoadScene(nomeDaFaseDestino);
        Debug.Log("Gema coletada! Fase: " + nomeDaFaseDestino);
    
    }

    private void FixedUpdate(){
        floatingTimer += Time.deltaTime;
        float floatingHeight = Mathf.Sin(floatingTimer * floatingSpeed) * 0.1f;
        transform.position = new Vector3(initialPosition.x, initialPosition.y + floatingHeight, initialPosition.z);
    }
}
