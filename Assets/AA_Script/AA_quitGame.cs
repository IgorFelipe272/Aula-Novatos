using UnityEngine;

public class AA_quitGame : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}