using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public string sceneToLoad;
    public float delayBeforeLoad = 2f;

    private void Start()
    {
        // Start the coroutine to load the scene after a delay
        StartCoroutine(LoadSceneAfterDelay());
    }

    private IEnumerator LoadSceneAfterDelay()
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delayBeforeLoad);

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }
    
}