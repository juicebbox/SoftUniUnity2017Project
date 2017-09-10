using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;

    public PlayerItems playerItems;

    public int Value { get { return value; } }

    private bool isTaken = false;
    private Collider2D col;


    private void Start()
    {
        col = GetComponent<Collider2D>();
        // It bugs if the colider is not restarted as it triggers the trigger... 
        // temporary solve.
        col.enabled = false;
        col.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !isTaken)
        {
            Physics2D.IgnoreCollision(this.col, col, false);
            isTaken = true;

            col.gameObject.GetComponent<PlayerItems>().AddCoins(Value);
            Destroy(gameObject);
        }
        else if (col.gameObject.layer != LayerMask.NameToLayer("Ground")) 
        {
            Physics2D.IgnoreCollision(this.col, col);
        }
    }
}
