using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int paperCount;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI paperCountText;
    //Singleton pattern
    void Awake() 
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        ResetPaperCount();
    }

    public void CollectPaper()
    {
        paperCount++;
        paperCountText.text = paperCount.ToString();
    }

    public void ProcessPlayerDeath()
    {
        ResetPaperCount();
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void ResetPaperCount()
    {
        paperCount = 0;
        paperCountText.text = paperCount.ToString();
    }

    void ResetGameSession()
    {
        FindAnyObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }
}
