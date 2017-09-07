using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour {

    [SerializeField]
    private BoxCollider2D platformCollider;

	void Start ()
    {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        platformCollider.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        platformCollider.enabled = true;
    }
}
