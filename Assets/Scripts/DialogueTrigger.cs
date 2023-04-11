using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    Image image;
    public Dialogue dialogue;
    bool start = false;

    void Awake()
    {
        image = GetComponentInChildren<Image>();
    }

    void Start()
    {
        image.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKey && !start)
        {
            start = true;
            if (start && dialogue.sequence == 0)
            {
                TriggerDialogue();
            }
        }
    }

    public void TriggerDialogue()
    {
        image.gameObject.SetActive(true);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    public void SetNextDialogue()
    {
        if (dialogue.sequence == 0)
        {
            Destroy(this.gameObject);
        }
        dialogue.sequence--;
        if (dialogue.sequence == 0)
        {
            TriggerDialogue();
        }
    }
}
