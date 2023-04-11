using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D myRigidBody2D;
    bool hasHit;
    bool hasHitEnemy;
    [SerializeField] AudioClip pewPewClip;
    [SerializeField] AudioClip explosionClip;
    [SerializeField] GameObject bleed;
    bool isBleeding = false;

    void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        AudioSource.PlayClipAtPoint(pewPewClip, Camera.main.transform.position, 0.7f);
    }

    void Update()
    {
        if (!hasHit)
        {
            if (myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Ground")))
            {
                myRigidBody2D.velocity = Vector2.zero;
                if (myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
                {
                    hasHit = true;
                    myRigidBody2D.isKinematic = true;
                }
                else
                {
                    hasHitEnemy = true;
                }
            }
            else
            {
                float angle = Mathf.Atan2(myRigidBody2D.velocity.y, myRigidBody2D.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && (myRigidBody2D.velocity != Vector2.zero || hasHitEnemy))
        {
            AudioSource.PlayClipAtPoint(explosionClip, Camera.main.transform.position, 0.3f);
            other.GetComponent<Animator>().SetTrigger("BeaDie");
            hasHitEnemy = false;
        }
        else if (other.tag == "BOSS" && (myRigidBody2D.velocity != Vector2.zero || hasHitEnemy))
        {
            if (!isBleeding)
            {
                isBleeding = true;
                Instantiate(bleed, transform.position + new Vector3(transform.position.x > 0 ? 0.5f : -0.5f, 0f, 0f), Quaternion.identity);
            }
            hasHitEnemy = false;
        }
        Destroy(gameObject, 0.5f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject, 0.5f);
    }
}
