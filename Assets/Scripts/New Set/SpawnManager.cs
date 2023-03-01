using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Prefabs; //array for objects to spawn

    public float rangeX; //to tell - and + range of x axis from origin
    public float rangeZ; //to tell - and + range of z axis from origin
    public float spawnHeight; //determines how high objects should spawn

    //for plotting spawn location in 3d space
    private float plotX;
    private float plotZ;

    //object chosen at random
    private GameObject objectSpawn;

    public LayerMask otherLayer;

    public void SpawnNextObject() //to be called by other game objects
    {
        plotX = Random.Range(rangeX * -1, rangeX);
        plotZ = Random.Range(rangeZ * -1, rangeZ);

        transform.position = new Vector3(plotX, spawnHeight, plotZ);

        objectSpawn = Prefabs[Random.Range(0, Prefabs.Length)];

        if (Physics.Raycast(transform.position, Vector3.down, 3.9f, otherLayer))
        {
            Debug.Log("Not ground layer");
        }
        else
        { 
            Instantiate(objectSpawn, transform.position, transform.rotation);
        }

        
    }
}
