using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EP_ChangeScene : MonoBehaviour
{
    [Header("Nome da próxima cena.")]
    public string nextScene;

    public void ChangeScene()
    {
        if(!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            Debug.LogWarning("O nome da próxima cena não foi definido.");
        }
    }
}
