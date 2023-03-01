using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Countdown : MonoBehaviour
{
    private Timer timer;
    public bool countdownActive = false;

    float currentTime;
    public float startSeconds;

    public GameObject countdownPanel;
    public Text currentTimeText;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startSeconds;
        countdownPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (countdownActive == true)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                EndCountdown();
                timer.StartTimer();
                Start();
            }
        }

        //TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = ((int)currentTime).ToString();
    }

    public void StartCountdown()
    {
        countdownPanel.SetActive(true);
        countdownActive = true;

    }
    public void EndCountdown()
    {
        countdownActive = false;
    }
}
