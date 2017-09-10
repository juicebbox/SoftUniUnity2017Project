using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndLevelScript : MonoBehaviour
{

    private bool isTriggered;
    private bool isEntering;

    public Text promptText;
    private GameMaster gameMaster;
    // Use this for initialization
    void Start ()
    {
        gameMaster = GameObject.Find("GameMaster").GetComponent<GameMaster>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isTriggered)
        {
            promptText.gameObject.SetActive(true);
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }

        if(Input.GetAxis("Use") != 0 && isTriggered && !isEntering)
        {
            promptText.gameObject.SetActive(false);
            isEntering = true;
        }

        if(isEntering && isTriggered)
        {
            gameMaster.upgradeMenu = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isTriggered = false;
        }
    }

}
