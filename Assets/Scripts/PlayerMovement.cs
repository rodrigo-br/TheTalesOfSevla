using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    [SerializeField] float speedOfMovement = 4f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 2f;
    [SerializeField] Bow bow;
    [SerializeField] AudioClip boing;
    [SerializeField] AudioClip ouch;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;
    PolygonCollider2D myPolygonCollider2D;
    float defaultGravity;
    bool isAlive;
    bool isShooting;
    bool isBossFight = false;
    [SerializeField] int numberOfArrows = 15;
    bool isOnEdge = false;

    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        myPolygonCollider2D = GetComponent<PolygonCollider2D>();
    }
    void Start()
    {
        defaultGravity = myRigidBody.gravityScale;
        isAlive = true;
        isShooting = false;
        FindObjectOfType<GameSession>().UpdateArrowText(numberOfArrows);
        if (SceneManager.GetActiveScene().name == "Boss")
        {
            isBossFight = true;
        }
    }

    void Update()
    {
        if (isAlive && !isShooting)
        {
            Run();
            FlipSprite();
            ClimbLadder();
            Die();
        }
    }

    void OnMove(InputValue value)
    {
        if (isAlive)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value)
    {
        if (isAlive)
        {
            if (value.isPressed && IsFeetTouching("Ground", "Enemy"))
            {
                myRigidBody.velocity += new Vector2(0f, jumpSpeed);
            }
        }
    }
    
    void OnFire(InputValue value)
    {
        if (isAlive && numberOfArrows > 0)
        {
            Shoot();
        }
    }

    void Run()
    {
        if (!isShooting)
        {
            Vector2 playerVelocity = new Vector2(moveInput.x * speedOfMovement, myRigidBody.velocity.y);
            myRigidBody.velocity = playerVelocity;
            CheckEdge();
            if (!isOnEdge)
            {
                myAnimator.SetBool("isRunning", IsMovingHorizontal());
            }
        }
    }

    void CheckEdge()
    {
        if (IsFeetTouching("Ground") && !myPolygonCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isOnEdge = true;
            myAnimator.SetBool("isOnEdge", true);
        }
        else
        {
            isOnEdge = false;
            myAnimator.SetBool("isOnEdge", false);
        }
    }

    void ClimbLadder()
    {
        if (IsFeetTouching("Ladder"))
        {
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
            myRigidBody.gravityScale = 0;
            myRigidBody.velocity = climbVelocity;
            myAnimator.SetBool("isClimbing", IsMovingVertical());
        }
        else
        {
            myRigidBody.gravityScale = defaultGravity;
            myAnimator.SetBool("isClimbing", false);
        }
    }

    void FlipSprite()
    {
        if (IsMovingHorizontal() && !isShooting)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    bool IsMovingHorizontal()
    {
        return Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
    }

    bool IsMovingVertical()
    {
        return Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
    }

    bool IsFeetTouching(params string[] layers)
    {
        return myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask(layers));
    }

    bool IsBodyTouching(params string[] layers)
    {
        return myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask(layers));
    }

    void Die()
    {
        if (IsBodyTouching("Enemy", "Hazards") || (transform.position.y < -5 && SceneManager.GetActiveScene().buildIndex == 2))
        {
            isAlive = false;
            AudioSource.PlayClipAtPoint(ouch, Camera.main.transform.position);
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = new Vector2(0f, jumpSpeed);
            FindObjectOfType<GameSession>().Invoke("ProcessPlayerDeath", 1f);
        }
    }

    void Shoot()
    {
        isShooting = true;
        myRigidBody.velocity = new Vector2(0f, myRigidBody.velocity.y);
        transform.localScale = new Vector2(Mathf.Sign(bow.transform.right.x), transform.localScale.y);
        myAnimator.SetTrigger("Shooting");
        bow.Invoke("Shoot", 0.2f);
        Invoke("FalseShooting", 0.2f);
        if (isBossFight)
        {
            numberOfArrows--;
            FindObjectOfType<GameSession>().UpdateArrowText(numberOfArrows);
        }
    }

    void FalseShooting()
    {
        isShooting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Jumper")
        {
            AudioSource.PlayClipAtPoint(boing, Camera.main.transform.position);
            other.GetComponent<Animator>().SetTrigger("Bounce");
        }
    }

    public void RestoreArrows(int value)
    {
        if (isBossFight)
        {
            numberOfArrows += value;
            FindObjectOfType<GameSession>().UpdateArrowText(numberOfArrows);
        }
    }

    public void ShakeCamera()
    {
        myAnimator.SetTrigger("Shake");
    }
}
