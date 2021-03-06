﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobsterController : MonoBehaviour {

    [SerializeField]
    private EnemyEyeSight enemyEyeSight;

    [SerializeField]
    private Transform[] patrolPoints;
    private int currentPoint = 0;

    [SerializeField]
    private float pointRange = 0.05f;
    [SerializeField]
    private float attackRange = 2f;

    [SerializeField]
    private float speed;

    private bool facingRight = false;
    private float movementX;
    private float movementY;
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

    private RandomItemDrop drop;

    private bool isDying;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<CharacterHealth>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        colider = GetComponent<Collider2D>();
        drop = GetComponent<RandomItemDrop>();
    }

    private void Update()
    {
        if (!enemyEyeSight.EnemySpotted && patrolPoints.Length != 0)
        {
            SetCurrentPointNumber();
        }
        
    }

    void FixedUpdate()
    {

        if (health != null && !health.IsDead)
        {

            if (!enemyEyeSight.EnemySpotted && patrolPoints.Length != 0)
            {
                Patrol(currentPoint);
            }
            else if (enemyEyeSight.EnemySpotted)
            {
                AttackPlayer();
            }
        }
        Flip();

        HandleAnimation();

        CheckIfDead();
    }

    private void AttackPlayer()
    {
        if (transform.position.x - player.transform.position.x <= -attackRange)
        {
            performingAttack = false;
            movementX = speed;
        }
        else if (transform.position.x - player.transform.position.x >= attackRange)
        {
            performingAttack = false;
            movementX = -speed;
        }
        else if (transform.position.x - player.transform.position.x > -attackRange || transform.position.x - player.transform.position.x < attackRange)
        {
            movementX = 0;
            performingAttack = true;
        }

        if (performingAttack && timeSinceLastAttack >= attackTime)
        {
            timeSinceLastAttack = 0;
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
        else if (performingAttack)
        {
            timeSinceLastAttack += Time.deltaTime;
        }
        rb.velocity = new Vector2(movementX, rb.velocity.y);
    }

    private void CheckIfDead()
    {
        if (health.IsDead)
        {
            colider.enabled = false;
            rb.isKinematic = true;
            Destroy(gameObject, 5f);
            rb.velocity = Vector2.zero;
            if (!isDying)
            {
                drop.DropItems();
                isDying = true;
            }
        }
    }

    private void SetCurrentPointNumber()
    {
        if ((transform.position.x - patrolPoints[currentPoint].position.x <= pointRange && !facingRight) || (transform.position.x - patrolPoints[currentPoint].position.x >= -pointRange && facingRight))
        {
            if (currentPoint < patrolPoints.Length - 1)
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
        if (patrolPoints[currentPoint].position.x - transform.position.x <= pointRange)
        {
            movementX = -speed;
        }
        else
        {
            movementX = speed;
        }
        rb.velocity = new Vector2(movementX, rb.velocity.y);
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

        anim.SetBool("IsAttacking", performingAttack);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.collider, true);
        }
    }
}
