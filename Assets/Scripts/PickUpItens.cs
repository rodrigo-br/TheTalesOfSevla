using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItens : MonoBehaviour
{
    [SerializeField] AudioClip toiletPaperClip;
    bool wasCollected = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().CollectPaper();
            AudioSource.PlayClipAtPoint(toiletPaperClip, Camera.main.transform.position, 0.4f);
            Destroy(gameObject);
        }
    }
}
