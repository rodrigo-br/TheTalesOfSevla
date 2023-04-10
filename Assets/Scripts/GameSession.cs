using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    int paperCount;
    [SerializeField] TextMeshProUGUI paperCountText;
    [SerializeField] TextMeshProUGUI arrowsCountText;
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
            FindObjectOfType<LivesImageManager>().DestroyImage(playerLives - 1);
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
    }
}
