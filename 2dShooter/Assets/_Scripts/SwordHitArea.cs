using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitArea : MonoBehaviour
{
    private bool enemyNear;
    public bool EnemyNear
    {
        get
        {
            return enemyNear;
        }
    }

    private CharacterHealth enemyHealth;

    public CharacterHealth EnemyHealth
    {
        get
        {
            return enemyHealth;
        }
    }

    void Start ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyNear = true;
            enemyHealth = collision.gameObject.GetComponent<CharacterHealth>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            enemyNear = false;
            enemyHealth = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyNear = true;
            enemyHealth = collision.gameObject.GetComponent<CharacterHealth>();
        }
    }
}
