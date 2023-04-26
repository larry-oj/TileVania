using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int playerScore = 0;
    
    private void Awake()
    {
        var numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IncrementScore(int score)
    {
        playerScore += score;
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLives(1);
            StartCoroutine(ReloadScene());
        }
        else
        {
            ResetGameSession();
        }
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TakeLives(int numOfLives)
    {
        playerLives -= numOfLives;
    }
    
    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
