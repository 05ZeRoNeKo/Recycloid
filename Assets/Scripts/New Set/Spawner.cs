using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private GameplayManager gameplayManager;
    private SpawnManager spawnManager;

    [Space]
    public bool canSpawn; //to control when to spawn
    public float timeToSpawn;
    private void Awake()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
        spawnManager = FindObjectOfType<SpawnManager>();
        canSpawn = true;
    }

    // Update is called once per frame
    [System.Obsolete]
    public void Update()
    {
        if (gameplayManager.isLevelStarted)
        {
            Spawn();
        }
    }

    [System.Obsolete]
    public void Spawn()
    {
        if (canSpawn)
        {
            canSpawn = false;
            spawnManager.SpawnNextObject();
            Invoke(nameof(ResetSpawn), timeToSpawn);
        }
    }
    public void ResetSpawn()
    {
        canSpawn = true;
    }
}
