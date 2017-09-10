using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{

    public float damage = 30;

    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidbody;
    private Vector2 direction;

    private Animator hitEffectAC;
    [SerializeField]
    private GameObject hitEffect;

	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        hitEffect.SetActive(false);
	}

    private void Update()
    {
        
    }

    void FixedUpdate ()
    {
        myRigidbody.velocity = direction * speed;
	}

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<CharacterHealth>().TakeDamage(damage);
            //myRigidbody.collisionDetectionMode = CollisionDetectionMode2D.None;
            hitEffect.SetActive(true);

            hitEffectAC = hitEffect.GetComponentInChildren<Animator>();
            speed = 0;
            Destroy(gameObject, 0.4f);
        }
    }
}
