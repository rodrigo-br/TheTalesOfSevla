using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [Range(0.1f, 10f)][SerializeField] float movSpeed = 3f;
    [SerializeField] float attackRange = 2.5f;
    Rigidbody2D bossRigidBody;
    Animator bossAnimator;
    float defaultMovSpeed;
    bool isFlipped = false;
    bool canDash = true;
    [SerializeField] float dashPower = 10f;
    [SerializeField] float dashTime = 0.2f;
    [SerializeField] float dashCooldown = 2f;
    [SerializeField] BuffItem itemBuffer;
    [SerializeField] EnemyMovement enemy;
    [SerializeField] LevelExit exit;
    bool canFlip = true;
    bool canTakeDamage = false;
    HealthBar healthBar;
    Transform child;
    int stage = 0;
    [SerializeField] int lifes = 10;
    void Awake()
    {
        bossAnimator = GetComponent<Animator>();
        bossRigidBody = GetComponent<Rigidbody2D>();
        healthBar = GetComponentInChildren<HealthBar>();
        child = this.gameObject.transform.GetChild(0);
    }

    void Start()
    {
        defaultMovSpeed = movSpeed;
        healthBar.Setup(new HealthSystem(lifes));
        InvokeRepeating("GenerateItemBuff", 5f, 8f);
    }

    void GenerateItemBuff()
    {
        Instantiate(itemBuffer, transform.position, transform.rotation);
    }

    void FixedUpdate()
    {
        if (canFlip)
        {
            FollowTarget();
            FlipSprite();
        }
    }

    void FlipSprite()
    {
        Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > target.position.x && isFlipped)
		{
			Flip(flipped, false);
		}
		else if (transform.position.x < target.position.x && !isFlipped)
		{
            Flip(flipped, true);
		}
    }

    void EnterIdle()
    {
        StopMoving();
        Invoke("RestoreMovemment", 1.5f);
    }

    void StopMoving()
    {
        movSpeed = 0;
        bossAnimator.SetBool("IsChasing", false);
    }

    void Flip(Vector3 vector, bool value)
    {
        EnterIdle();
        transform.localScale = vector;
		transform.Rotate(0f, 180f, 0f);
        child.Rotate(0f, 180f, 0f);
        isFlipped = value;
    }

    void RestoreMovemment()
    {
        bossAnimator.SetBool("IsChasing", true);
        movSpeed = defaultMovSpeed;
    }

    void FollowTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, movSpeed * Time.fixedDeltaTime);
        if (Mathf.Abs(transform.position.x - target.position.x) <= attackRange && canDash)
        {
            StartCoroutine("Dash");
        }
    }

    IEnumerator Dash()
    {
        canFlip = false;
        bossAnimator.SetTrigger("Attack");
        bossAnimator.SetBool("IsChasing", false);
        float flipDirection = isFlipped ? transform.localScale.x : -transform.localScale.x;
        canDash = false;
        canTakeDamage = true;
        if (stage >= 1)
        {
            Instantiate(itemBuffer, transform.position, transform.rotation);
        }
        if (stage >= 2)
        {
            Instantiate(enemy, transform.position - new Vector3(1f, 1.7f, 0f), Quaternion.identity).TurnMovementAround();
            Instantiate(enemy, transform.position - new Vector3(-1f, 1.7f, 0f), Quaternion.identity);
        }
        bossRigidBody.velocity = new Vector2(flipDirection * dashPower, 0f);
        yield return new WaitForSecondsRealtime(dashTime);
        yield return new WaitForSecondsRealtime(dashCooldown);
        bossAnimator.SetBool("IsChasing", true);
        canFlip = true;
        canDash = true;
        canTakeDamage = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Arrow" && canTakeDamage)
        {
            healthBar.healthSystem.Damage(1);
            healthBar.UpdateBar();
            if (healthBar.healthSystem.GetHealthPoints() <= 0)
            {
                CheckStage();
            }
        }
    }

    void CheckStage()
    {
        if (stage == 0)
        {
            healthBar.healthSystem.SetMaxHP(20);
            healthBar.healthSystem.Heal(20);
            CancelInvoke("GenerateItemBuff");
            movSpeed += 1f;
            defaultMovSpeed += 1f;
            attackRange += 0.3f;
            dashCooldown -= 0.2f;
            stage++;
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0.1f, 0.3f, 0.3f, 0.0f);
        }
        else if (stage == 1)
        {
            healthBar.healthSystem.SetMaxHP(30);
            healthBar.healthSystem.Heal(30);
            movSpeed += 0.5f;
            defaultMovSpeed += 0.5f;
            attackRange += 0.2f;
            dashCooldown -= 0.1f;
            stage++;
            gameObject.GetComponent<SpriteRenderer>().color -= new Color(0.1f, 0.3f, 0.3f, 0.0f);
        }
        else
        {
            DramaticDeath();
        }
        healthBar.UpdateBar();
    }

    void DramaticDeath()
    {
        Instantiate(exit, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
