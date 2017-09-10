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
    private bool rangedAttack;
    private float attackTime = 0.66f;
    private float attackTimer = 0;

    [SerializeField]
    private Transform groundCheck;
    private bool isGrounded;
    [SerializeField]
    private LayerMask groundLayerMask;
    [SerializeField]
    private float jumpPower = 10f;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform gunPoint;

    private new AudioSource audio;

    [SerializeField]
    private AudioClip gunAudio;
    [SerializeField]
    private AudioClip swordAudio;
    [SerializeField]
    private AudioClip jumpAudio;
    [SerializeField]
    private AudioClip walkAudio;

    private PlayerHealth health;

    [SerializeField]
    private SwordHitArea swordHitArea;

    [SerializeField]
    private float swordDamage = 20f;
    private Collider2D playerCollider;

    private GameMaster gameMaster;

    private float deadTimer = 0;
    private float dyingTime = 5f;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        health = GetComponent<PlayerHealth>();
        playerCollider = GetComponent<Collider2D>();
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }

    private void Update()
    {
        movementX = Input.GetAxis("Horizontal") * movementSpeed;
        movementY = rb.velocity.y;
        if(rb.velocity.y > jumpPower)
        {
            movementY = jumpPower;
        }
    }

    void FixedUpdate ()
    {
        if (!health.IsDead)
        {

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.15f, groundLayerMask);

            // Play move sound
            if (isGrounded && movementX != 0)
            {
                if(!audio.isPlaying)
                {
                    audio.PlayOneShot(walkAudio);
                }
            }

            HandleInput();
            Flip();
            HandleAttacks();
        }
        else
        {
            deadTimer += Time.deltaTime;

            if(deadTimer > dyingTime)
            {
                gameMaster.gameStarted = false;
                Time.timeScale = 0;
            }
        }
        HandleAnimation();
    }

    private void HandleInput()
    {
        // melee attack
        if(Input.GetAxisRaw("Fire1") > 0 && !meleeAttack)
        {
             meleeAttack = true;
        }
        // ranged attack
        if (Input.GetAxisRaw("Fire2") > 0 && !rangedAttack)
        {
            rangedAttack = true;
        }
        // movement
        if (!(meleeAttack))
        {
            rb.velocity = new Vector2(movementX, rb.velocity.y);
        }

        // Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            audio.PlayOneShot(jumpAudio);
        }
    }


    private void HandleAnimation()
    {
        // Running Animation On/Off
        anim.SetBool("IsRunning", movementX != 0);

        // Attack animation trigger
        anim.SetBool("MeleeAttack", meleeAttack);
        anim.SetBool("IsJumping", !isGrounded);
        anim.SetBool("IsShooting", rangedAttack);
        anim.SetBool("IsDead", health.IsDead);
    }
    private void HandleAttacks()
    {
        if(meleeAttack)
        {
            if(attackTimer == 0)
            {
                audio.PlayOneShot(swordAudio);
                if(swordHitArea.EnemyNear)
                {
                    swordHitArea.EnemyHealth.TakeDamage(swordDamage);
                }
            }

            attackTimer += Time.deltaTime;

            if (isGrounded)
            {
                movementX = 0;
                movementY = rb.velocity.y;
            }

            if (attackTime < attackTimer)
            {
                attackTimer = 0;
                meleeAttack = false;
            }
        }

        if(rangedAttack)
        {
            if(attackTimer == 0)
            {
                ShootGun();
            }
            attackTimer += Time.deltaTime;
            if (attackTime < attackTimer)
            {
                attackTimer = 0;
                rangedAttack = false;
            }
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

    public void ShootGun()
    {
        if(facingRight)
        {
            GameObject tempBullet = Instantiate(bulletPrefab, gunPoint.position, gunPoint.rotation);

            tempBullet.GetComponent<Bullet>().SetDirection(Vector2.right);
        }
        else
        {
            GameObject tempBullet = Instantiate(bulletPrefab, gunPoint.position, Quaternion.Euler(new Vector3(0, 0, 180)));

            tempBullet.GetComponent<Bullet>().SetDirection(Vector2.left);
        }
        audio.PlayOneShot(gunAudio);
    }

}
