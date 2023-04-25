using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private float loadDelaySeconds = 1f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided! - " + other.gameObject.CompareTag("Player"));
        if (other.gameObject.CompareTag("Player"))
            StartCoroutine(LoadNextLevel());
    }
    
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(loadDelaySeconds);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
