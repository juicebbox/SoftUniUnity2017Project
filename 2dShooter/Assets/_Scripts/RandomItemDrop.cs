using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemsToDrop;
    [SerializeField]
    private float[] percentChanceToDrop;
    [SerializeField]
    private float dropRange = 1f;
    [SerializeField]
    private Transform dropSpot;
    [SerializeField]
    private float[] itemMultiplier;

	// Use this for initialization
	void Start ()
    {
		
	}

    public void DropItems()
    {
        for (int i = 0; i < itemsToDrop.Length; i++)
        {
            float rand = Random.Range(0, 100);
            float placeToDrop = Random.Range(-dropRange, dropRange) + dropSpot.position.x;

            if (rand <= percentChanceToDrop[i])
            {
                Vector3 dropLocation = new Vector3(placeToDrop, dropSpot.position.y, dropSpot.position.z);

                GameObject newItem = Instantiate(itemsToDrop[i], dropLocation, dropSpot.rotation).gameObject;
                newItem.transform.parent = null;
            }
        }
    }
}
