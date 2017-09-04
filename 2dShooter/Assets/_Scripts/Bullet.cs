using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody2D myRigidbody;
    private Vector2 direction;

	void Start ()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
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
}
