using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    public void PlayAgain()
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
}
