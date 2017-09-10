using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestScript : MonoBehaviour
{

    private bool isTriggered;
    private RandomItemDrop itemDrop;

    private Animator chestAnim;

    public Text promptText;
    private bool hasDropped;
    private BoxCollider2D col;


    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        itemDrop = GetComponent<RandomItemDrop>();
        chestAnim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!hasDropped)
        {
            if (isTriggered)
            {
                promptText.gameObject.SetActive(true);
            }
            else
            {
                promptText.gameObject.SetActive(false);
            }
            if (Input.GetAxis("Use") != 0)
            {
                itemDrop.DropItems();
                hasDropped = true;
                promptText.gameObject.SetActive(false);
                chestAnim.SetBool("open", true);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            isTriggered = true;
        }
        if (col.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            Physics2D.IgnoreCollision(this.col, col, true);
        }
    }
    //private void OnTriggerStay2D(Collider2D col)
    //{
    //    if (col.gameObject.tag != "Ground")
    //    {
    //        Physics2D.IgnoreCollision(this.col, col, true);
    //    }
    //}

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            isTriggered = false;
        }
    }
}
