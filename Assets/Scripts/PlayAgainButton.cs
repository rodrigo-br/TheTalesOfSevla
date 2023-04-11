using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    public void PlayAgain()
    {
        Destroy(FindObjectOfType<GameSession>().gameObject);
        SceneManager.LoadScene(0);
    }
}
