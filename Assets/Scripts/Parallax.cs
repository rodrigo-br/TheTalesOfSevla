using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] float moveXFactor;
    [SerializeField] float moveYFactor;
    [SerializeField] Transform cam;
    [SerializeField] bool lockY = false; 

    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(cam.position.x * moveXFactor, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(cam.position.x * moveXFactor, cam.position.y * moveYFactor);
        }
    }
}
