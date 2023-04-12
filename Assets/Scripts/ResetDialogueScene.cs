using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDialogueScene : MonoBehaviour
{
    void Start()
    {
        GameSession gameSession = FindAnyObjectByType<GameSession>();
        if (gameSession != null)
        {
            Destroy(gameSession.gameObject);
        }
        ScenePersist scenePersist = FindAnyObjectByType<ScenePersist>();
        if (scenePersist != null)
        {
            Destroy(scenePersist.gameObject);
        }
    }
}
