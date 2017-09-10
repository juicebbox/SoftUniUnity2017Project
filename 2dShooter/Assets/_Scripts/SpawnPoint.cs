using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject spawnPrefab;

    public float spawnTime;

    public int mobsToSpawn;

    private float timeSinceLastSpawn;

    private float offsetRange = 3;

    void Start ()
    {
        timeSinceLastSpawn = 0;

	}
	
	// Update is called once per frame
	void Update ()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn > spawnTime)
        {
            float offsetX = Random.Range(-offsetRange, offsetRange);

            Instantiate(spawnPrefab, new Vector3(transform.position.x + offsetX,transform.position.y, transform.position.z), transform.rotation, transform.parent);
            timeSinceLastSpawn = 0;
            mobsToSpawn -= 1;
        }

        if (mobsToSpawn == 0)
        {
            Destroy(gameObject, 3f);
        }
	}
}
