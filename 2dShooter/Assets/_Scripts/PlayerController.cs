using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 5;

    private Rigidbody2D rb;
    private Animator anim;
    private float movementX;
    private float movementY;
    private bool facingRight = true;

    private bool meleeAttack;
    private bool switchToRanged;
    private float attackTime = 0.66f;
    private float attackTimer = 0;

    [SerializeField]
    private Transform groundCheck;
    private bool isGrounded;
    [SerializeField]
    private LayerMask groundLayerMask;
    [SerializeField]
    private float jumpPower = 10f;
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        movementX = Input.GetAxis("Horizontal") * movementSpeed;
        movementY = rb.velocity.y;
    }

    void FixedUpdate ()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.20f, groundLayerMask);
        HandleInput();
        Flip();
        HandleAnimation();
        HandleAttacks();
    }

    private void HandleInput()
    {

        if(Input.GetAxisRaw("Fire1") > 0 && !meleeAttack)
        {
            meleeAttack = true;
        }
        // movement
        if(!meleeAttack)
        {
            rb.velocity = new Vector2(movementX, movementY);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        // Jump
        if(Input.GetAxisRaw("Jump") > 0 && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }


    private void HandleAnimation()
    {
        // Running Animation On/Off
        if (movementX != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        // Attack animation trigger
        anim.SetBool("MeleeAttack", meleeAttack);
        anim.SetBool("IsJumping", !isGrounded);
    }
    private void HandleAttacks()
    {
        if(meleeAttack && isGrounded)
        {
            attackTimer += Time.deltaTime;

            Debug.Log(isGrounded);
            movementX = 0;
            movementY = 0;

            if (attackTime < attackTimer)
            {
                attackTimer = 0;
                meleeAttack = false;
            }
        }
        else
        {
            meleeAttack = false;
        }
    }

    private void Flip()
    {
        if((facingRight && movementX < 0) || (!facingRight && movementX > 0))
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            facingRight = !facingRight;
        }
    }
}
