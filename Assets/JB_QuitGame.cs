using UnityEngine;

public class JB_QuitGame : MonoBehaviour
{
    public void QuitGame(){
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}
