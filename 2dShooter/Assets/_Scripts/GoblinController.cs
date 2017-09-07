using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    [SerializeField]
    private EnemyEyeSight enemyEyeSight;

    [SerializeField]
    private Transform[] patrolPoints;
    private int currentPoint = 0;

    private float pointRange = 0.05f;
    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private float speed;

    private bool facingRight = false;
    private float movementX;
    private Rigidbody2D rb;
    private CharacterHealth health;
    private Animator anim;
    private GameObject player;
    private Collider2D colider;

    private bool performingAttack;

    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float attackTime;
    private float timeSinceLastAttack = 0;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<CharacterHealth>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        colider = GetComponent<Collider2D>();
    }
	
	void FixedUpdate ()
    {
        if(!health.IsDead)
        {
            if (!enemyEyeSight.EnemySpotted)
            {
                SetCurrentPointNumber();
                Patrol(currentPoint);
            }
            else if (enemyEyeSight.EnemySpotted)
            {
                AttackPlayer();
            }
        }

        // He looks at you until he dies
        Flip();

        HandleAnimation();

        CheckIfDead();
    }

    private void AttackPlayer()
    {
        if(transform.position.x - player.transform.position.x <= -attackRange)
        {
            performingAttack = false;
            movementX = speed;
        }
        else if(transform.position.x - player.transform.position.x >= attackRange)
        {
            performingAttack = false;
            movementX = -speed;
        }
        else if(transform.position.x - player.transform.position.x > -attackRange || transform.position.x - player.transform.position.x < attackRange)
        {
            movementX = 0;
            performingAttack = true;
        }

        if(performingAttack && timeSinceLastAttack >= attackTime)
        {
            timeSinceLastAttack = 0;
            player.GetComponent<CharacterHealth>().TakeDamage(attackDamage);
        }
        else if(performingAttack)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        rb.velocity = new Vector2(movementX, 0);
    }

    private void CheckIfDead()
    {
        if(health.IsDead)
        {
            colider.enabled = false;
            rb.isKinematic = true;
            Destroy(gameObject, 5f);
        }
    }

    private void SetCurrentPointNumber()
    {
        if((transform.position.x - patrolPoints[currentPoint].position.x <= pointRange && !facingRight) || (transform.position.x - patrolPoints[currentPoint].position.x >= -pointRange && facingRight))
        {
            if(currentPoint < patrolPoints.Length - 1)
            {
                currentPoint++;
            }
            else
            {
                currentPoint = 0;
            }
        }
    }

    private void Patrol(int currentPoint)
    {
        if(patrolPoints[currentPoint].position.x < transform.position.x)
        {
            movementX = -speed;
        }
        else if(patrolPoints[currentPoint].position.x > transform.position.x)
        {
            movementX = speed;
        }
        else
        {
            movementX = 0;
        }
        rb.velocity = new Vector2(movementX, 0);
    }

    private void Flip()
    {
        if ((facingRight && movementX < 0) || (!facingRight && movementX > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = !facingRight;
        }
    }

    private void HandleAnimation()
    {

        anim.SetBool("IsWalking", movementX != 0);
        anim.SetBool("IsAttacking", performingAttack);
        if(health.IsDead)
        {
            anim.SetBool("IsDying", true);
        }
    }
}
