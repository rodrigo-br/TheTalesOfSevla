using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidBody2D;

    void Awake()
    {
        myRigidBody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        myRigidBody2D.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform")
        {
            TurnMovementAround();
            FlipEnemyFacing();
        }
    }

    public void TurnMovementAround()
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody2D.velocity.x)), 1f);
    }
}
