using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{

    public void RestartEverything()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            Destroy(gameSession.gameObject);
        }
        SceneManager.LoadScene(0);
    }
    public void PlayLevel1()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession != null)
        {
            Destroy(gameSession.gameObject);
        }
        SceneManager.LoadScene(1);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void PlayLevel3()
    {
        SceneManager.LoadScene(3);
    }

    public void PlayBossLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void PlayBossLevelEasy()
    {
        SceneManager.LoadScene(9);
    }

    public void PlayLevel1Easy()
    {
        SceneManager.LoadScene(6);
    }

    public void PlayLevel2Easy()
    {
        SceneManager.LoadScene(7);
    }

    public void PlayLevel3Easy()
    {
        SceneManager.LoadScene(8);
    }
}
