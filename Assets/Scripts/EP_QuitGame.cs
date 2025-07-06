using UnityEngine;

public class EP_QuitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo.");
        Application.Quit();
    }
}
