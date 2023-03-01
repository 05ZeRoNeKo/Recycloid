using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinBehavior : MonoBehaviour
{
    private ScoreManager scoreManager;
    //private HealthManager healthManager;

    public AudioSource plusSFX;
    public AudioSource minusSFX;

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        //healthManager = FindObjectOfType<HealthManager>();
    }

    private void OnTriggerEnter(Collider collider) //is called when an object hits collider
    {
        if (collider.CompareTag(this.tag)) //checks tag of pickup item
        {
            //add score during game
            int scoreValue = collider.gameObject.GetComponent<ScoreValue>().scoreAdd;
            scoreManager.AddScore(scoreValue);

            plusSFX.Play();
            //add health during game
            //int healthValue = collision.gameObject.GetComponent<ScoreValue>().healthAdd;
            //healthManager.AddHealth(healthValue);

            Destroy(collider.gameObject);
        }
        else if (collider.CompareTag("Player"))
        {
            return;
        }
        else
        {
            //subtract score during game
            int scoreDeduction = collider.gameObject.GetComponent<ScoreValue>().scoreDeduct;
            scoreManager.SubstractScore(scoreDeduction);

            minusSFX.Play();
            //subtract health during game
            //int healthDeduction = collision.gameObject.GetComponent<ScoreValue>().healthDeduct;
            //healthManager.SubstractHealth(healthDeduction);

            Destroy(collider.gameObject);
        }
    }
}