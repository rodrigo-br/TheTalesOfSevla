using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItem : MonoBehaviour
{
    [SerializeField] GameObject particles;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity += new Vector2(Random.Range(-5, 5), Random.Range(2, 5));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().PickArrows();
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
