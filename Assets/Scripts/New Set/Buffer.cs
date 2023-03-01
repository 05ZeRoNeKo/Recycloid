using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
    public GameObject levelTrigger;
    public GameObject player;
    public float distanceFromCenter;

    public void PlayerCheck()
    {
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > distanceFromCenter)
        {
            gameObject.SetActive(false);
            levelTrigger.gameObject.SetActive(true);
        }
        else
        {
            levelTrigger.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }

    /*private void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, player.transform.position) > gameObject.GetComponent<SphereCollider>().radius)
        {
            gameObject.SetActive(true);
            levelTrigger.gameObject.SetActive(false);
        }
        else
        {
            levelTrigger.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            levelTrigger.gameObject.SetActive(true);
            gameObject.SetActive(false);

            
        }
        else
        {
            gameObject.SetActive(true);
            levelTrigger.gameObject.SetActive(false);
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        levelTrigger.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
