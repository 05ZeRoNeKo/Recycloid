using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    private Countdown countdown;
    private GameplayManager gameplayManager;

    private void Awake()
    {
        countdown = FindObjectOfType<Countdown>();
    }
    public void OnTriggerEnter(Collider other)
    {
        countdown.StartCountdown();
        gameObject.SetActive(false);
    }
}
