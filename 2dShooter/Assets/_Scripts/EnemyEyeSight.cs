using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeSight : MonoBehaviour
{
    private bool enemySpotted;

    [SerializeField]
    private float chaseTime = 2f;
    private float chasingForTime = 0;

    private bool chasingPlayerOutsideEyeSight;

    public bool EnemySpotted
    {
        get
        {
            return enemySpotted;
        }
    }

    private void Update()
    {
        ChasePlayer();

        if (chasingForTime >= chaseTime)
        {
            chasingPlayerOutsideEyeSight = false;
            chaseTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if(collision.gameObject.tag == "Player")
        {
            enemySpotted = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            chasingPlayerOutsideEyeSight = true;
            enemySpotted = false;

        }
    }

    private void ChasePlayer()
    {
        if(chasingPlayerOutsideEyeSight)
        {
            chasingForTime += Time.deltaTime;
        }
    }
}
