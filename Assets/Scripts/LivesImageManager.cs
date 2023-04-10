using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesImageManager : MonoBehaviour
{
    [SerializeField] Image[] image;

    public void DestroyImage(int index)
    {
        image[index].enabled = false;
    }
}
