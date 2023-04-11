using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusic : MonoBehaviour
{
    [SerializeField] AudioClip normalClip;
    [SerializeField] AudioClip bossClip;
    private static BGMusic instance = null;
    bool isBoss = false;
    public static BGMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return ;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void FixedUpdate()
    {
        if (!isBoss && SceneManager.GetActiveScene().name == "Boss")
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            isBoss = true;
            audioSource.Stop();
            audioSource.clip = bossClip;
            audioSource.Play();
        }
        if (isBoss && SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1)
        {
            AudioSource audioSource = GetComponent<AudioSource>();
            isBoss = false;
            audioSource.Stop();
            audioSource.clip = normalClip;
            audioSource.Play();
        }
    }
}
