using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    private GameplayManager gameplayManager;
    bool timerActive = false;

    float currentTime;
    public float startMinutes;

    public GameObject timePanel;
    public Text currentTimeText;

    private void Awake()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startMinutes * 60;
        timePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive == true)
        {
            currentTime -= Time.deltaTime;

            if(currentTime <= 0)
            {
                EndTimer();
                Start();
            }
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        timePanel.SetActive(true);
        gameplayManager.levelStart();
        timerActive = true;
    }
    public void EndTimer()
    {
        timerActive = false;
        gameplayManager.levelEnd();
    }
}
