using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashChute : MonoBehaviour
{
    private GameManager gameManager;

    public GameObject[] spawnPoints; //where pickup items will spawn within the chute
    public GameObject[] trashPrefabs; //the items that will be instantiated or spawned

    [Space]
    public bool canSpawn; //to control when to spawn
    public float timeToSpawn;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    [System.Obsolete]
    public void Update()
    {
        if (gameManager.isGameStarted)
        {
            Spawner();
        }
    }
    [System.Obsolete]
    public void Spawner()
    {
        if (canSpawn)
        {
            canSpawn = false;
            Spawn();
            Invoke(nameof(ResetSpawn), timeToSpawn);
        }
    }
    [System.Obsolete]
    public void Spawn() //spawner method
    {
        GameObject spawnPos = spawnPoints[Random.RandomRange(0, spawnPoints.Length)]; //randomly selects a spawn point
        GameObject trashObj = trashPrefabs[Random.RandomRange(0, trashPrefabs.Length)]; //randomly selects a trash/pickup item

        Instantiate(trashObj, spawnPos.transform.position, spawnPos.transform.rotation);  
    }
    public void ResetSpawn()
    {
        canSpawn = true;
    }
}
