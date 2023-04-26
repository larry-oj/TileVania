using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private float loadDelaySeconds = 1f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(LoadNextLevel());
    }
    
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadDelaySeconds);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        var nextSceneIndex = currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1 ? 0 : currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
