using UnityEngine;

public class IF_QuitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
