using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    private Countdown countdown;
    private GameplayManager gameplayManager;

    public GameObject scorePanel;
    public Text scoreText;
    public int currentScore;
    public int highScore;
    public int endLevelScore;

    public GameObject finalScorePanel;
    public Text highScoreText;
    public Text endLevelScoreText;


    private void Awake()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
        countdown = FindObjectOfType<Countdown>();
    }
    private void Start()
    {
        scorePanel.SetActive(false);
        //finalScorePanel.SetActive(false);
        currentScore = 0;
        scoreText.text = currentScore.ToString();
    }

    private void Update()
    {
        if(countdown.countdownActive == true)
        {
            finalScorePanel.SetActive(false);
        }

        if (gameplayManager.isLevelStarted)
        {
            scorePanel.SetActive(true);
            SetEndLevelScore();
        }
        else
        {
            SetHighScore();
            Start();
        }
    }

    public void AddScore(int scoreAdd)
    {
        if (gameplayManager.isLevelStarted)
        {
            currentScore += scoreAdd;
            scoreText.text = currentScore.ToString();
        }
    }
    public void SubstractScore(int scoreDeduct)
    {
        if (gameplayManager.isLevelStarted)
        {
            currentScore -= scoreDeduct;
            scoreText.text = currentScore.ToString();

            if(currentScore <= 0)
            {
                currentScore = 0;
                scoreText.text = currentScore.ToString();
            }
        }
    }

    public void SetEndLevelScore()
    {
        endLevelScore = currentScore;
    }
    public void SetHighScore()
    {
        string levelName = "hs" + SceneManager.GetActiveScene().name;

        highScore = PlayerPrefs.GetInt(levelName, highScore);

        if(currentScore > highScore)
        {
            highScore = currentScore;

            PlayerPrefs.SetInt(levelName, highScore);
            //Debug.Log("HighScore: " + PlayerPrefs.GetInt(levelName, highScore).ToString());
        }
        
        
    }
    public void ShowFinalScore()
    {
        finalScorePanel.SetActive(true);

        highScoreText.text = highScore.ToString();

        endLevelScoreText.text = endLevelScore.ToString();
    }
}
