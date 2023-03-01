using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private static HealthManager instance;
    private GameManager gameManager;

    public GameObject healthUI;
    public Text healthText;

    public int currentHealth; //shows health for each game session

    public int minHealth; //minimum amount of health (which should be zero)
    public int maxHealth; //maximum amount of health

    private void Awake()
    {
        instance = this;
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        if (!gameManager.isTutorial)
        {
            healthUI.SetActive(true);
            currentHealth = maxHealth;
            healthText.text = currentHealth.ToString();
        }
        else
        {
            healthUI.SetActive(false);
        }
    }

    public void AddHealth(int healthAdd)
    {
        if (!gameManager.isTutorial && gameManager.isGameStarted)
        {
            currentHealth += healthAdd;
            healthText.text = currentHealth.ToString();

            if(currentHealth >= maxHealth) //to prevent exceeding maximum health
            {
                currentHealth = maxHealth;
                healthText.text = currentHealth.ToString();
            }
        }
    }
    public void SubstractHealth(int healthDeduct)
    {
        if (!gameManager.isTutorial && gameManager.isGameStarted)
        {
            currentHealth -= healthDeduct;
            healthText.text = currentHealth.ToString();

            if(currentHealth <= minHealth)
            {
                gameManager.EndGame();
                currentHealth = maxHealth;
                healthText.text = currentHealth.ToString();
            }
        }
    }
}
