using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightRoomController : MonoBehaviour
{
    public GameObject leftWall;
    public GameObject rightWall;

    public SpawnPoint[] spawnPoints;
    public Text fightText;

    public int enemyObjectCount;
    private bool roomLocked;
    private bool activatedSpawns;
    private float timeSinceStart;

    private RandomItemDrop itemDrop;
    private float maxSpawnTime;
    private float destroyTimer;
    
    // Use this for initialization
	void Start ()
    {
        itemDrop = GetComponent<RandomItemDrop>();
        leftWall.SetActive(false);
        rightWall.SetActive(false);
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i].gameObject.SetActive(false);
            enemyObjectCount += spawnPoints[i].mobsToSpawn;
            if(spawnPoints[i].spawnTime > maxSpawnTime)
            {
                maxSpawnTime = spawnPoints[i].spawnTime;
            }
        }
        fightText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update ()
    {

        if (enemyObjectCount == 0)
        {
            destroyTimer += Time.deltaTime;
            if(destroyTimer > maxSpawnTime)
            {
                itemDrop.DropItems();
                Destroy(gameObject);
            }
        }
        if (roomLocked)
        {
            ActivateSpawns();
        }
        if (activatedSpawns)
        {
            timeSinceStart += Time.deltaTime;
        }

        GetEnemiesCount();

        if (timeSinceStart > 2f)
        {
            fightText.text = enemyObjectCount.ToString() + " enemies coming";
        }
    }

    private void GetEnemiesCount()
    {
        enemyObjectCount = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            enemyObjectCount += spawnPoints[i].mobsToSpawn;
        }
    }

    private void ActivateSpawns()
    {
        if(!activatedSpawns)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                spawnPoints[i].gameObject.SetActive(true);
            }
            activatedSpawns = true;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            leftWall.SetActive(true);
            rightWall.SetActive(true);
            roomLocked = true;
            fightText.gameObject.SetActive(true);
        }        
    }
}
