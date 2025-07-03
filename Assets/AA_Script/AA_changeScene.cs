using UnityEngine;
using UnityEngine.SceneManagement;

public class AA_changeScene : MonoBehaviour
{
    [Header("Nome da pr�xima cena")]
    public string nextScene;

    public void ChangeScene()
    {
        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("O nome da pr�xima cena n�o foi definido.");
        }
    }
}