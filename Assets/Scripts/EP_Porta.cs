using UnityEngine;
using UnityEngine.SceneManagement;

public class EP_Porta : MonoBehaviour
{
    public EP_HealthSystem healthSystem;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool porta = healthSystem.portaLiberada;
        if (collision.CompareTag("Player") && porta)
        { 
            SceneManager.LoadScene("Fase_2");
        }
    }
}