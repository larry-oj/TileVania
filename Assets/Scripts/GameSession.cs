using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Header("Default Player Settings")]
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int playerScore = 0;
    
    [Header("UI Text References")]
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI scoreText;
    
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
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }

    public void IncrementScore(int score)
    {
        playerScore += score;
        scoreText.text = playerScore.ToString();
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
        livesText.text = playerLives.ToString();
    }
    
    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
