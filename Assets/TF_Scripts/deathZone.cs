using UnityEngine;
using UnityEngine.SceneManagement;

public class deathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reinicia a cena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            // ou: Destroy(other.gameObject);
        }
    }
}
