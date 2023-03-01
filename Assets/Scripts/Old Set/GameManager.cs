using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private SpawnManager spawnManager;

    [Header("Dev Level Selector")]
    public bool isTutorial;
    public bool isConveyorBelt;
    public bool isTreasureHunt;

    [Space]
    public bool isGameStarted;

    [Space]
    [Header("UI Panels")]
    public GameObject controlPanel;
    public GameObject gameStartPanel;
    public GameObject gameOverPanel;
    public GameObject newHighscoreUI;
    public Text newHighscoreText;
    public GameObject oldHighscoreUI;
    public Text oldHighscoreText;
    public Text playerScoreText;
    public GameObject scoreAndHealthPanel;

    [Space]
    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject restartConfirmMenu;
    public GameObject returnConfirmMenu;
    public GameObject mainMenu;

    [Space]
    [Header("Dialogue UI")]
    public Text dialogueText;


    [SerializeField]
    private int index;
    [SerializeField]
    private float textSpeed;
    [SerializeField]
    private string[] gameStartDialogue;

    private PlayerControls playerControls;

    private void Awake()
    {
        instance = this;
        spawnManager = FindObjectOfType<SpawnManager>();

        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        if (!isTutorial)
        {
            isGameStarted = false;

            controlPanel.SetActive(false);
            gameOverPanel.SetActive(false);
            scoreAndHealthPanel.SetActive(false);
            pauseMenu.SetActive(false);
            settingsMenu.SetActive(false);
            restartConfirmMenu.SetActive(false);
            returnConfirmMenu.SetActive(false);

            dialogueText.text = string.Empty;
            gameStartPanel.SetActive(true);
            StartDialogue();
        }
    }
    private void Update() //to check touch input and manage next line
    {
        if (!isTutorial)
        {
            if (playerControls.Player.Continue.WasReleasedThisFrame())
            {
                if (gameStartPanel.activeInHierarchy)
                {
                    if (dialogueText.text == gameStartDialogue[index])
                    {
                        if (!isGameStarted)
                        {
                            NextLine(gameStartDialogue);
                        }
                    }
                    else
                    {
                        StopAllCoroutines();

                        if (!isGameStarted)
                        {
                            dialogueText.text = gameStartDialogue[index];
                        }
                    }
                }
            }
        }
    }

    private void StartDialogue() //start of dialogue system
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine() //a method to add characters to the dialogue one-by-one, animation of sorts
    {
        if (!isGameStarted)
        {
            foreach (char c in gameStartDialogue[index].ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }
    private void NextLine(string[] dialogue) //to begin next line in dialogue system
    {
        if(index < dialogue.Length - 1)
        {
            index += 1;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            if (!isGameStarted)
            {
                StartGame();
            }
        }
    }
     

    //handles game state
    public void StartGame()
    {
        isGameStarted = true;

        gameStartPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        controlPanel.SetActive(true);
        scoreAndHealthPanel.SetActive(true);

        if (isTreasureHunt)
        {
            spawnManager.SpawnNextObject();
        }
    }
    public void EndGame()
    {
        isGameStarted = false;

        controlPanel.SetActive(false);
        scoreAndHealthPanel.SetActive(false);

        if (isConveyorBelt)
        {
            try
            {
                Rigidbody[] currentTrash = FindObjectsOfType<Rigidbody>();

                foreach(Rigidbody rb in currentTrash)
                {
                    if (!rb.gameObject.GetComponent<ConveyorBelt>())
                    {
                        Destroy(rb.gameObject);
                    }
                }
            }
            catch
            {
                return;
            }
        }
    }


    
    //handles time scale and menus in game
    public void Pause()
    {
        Time.timeScale = 0f; // stops time ticking in game

        //handles which menu shows
        controlPanel.SetActive(false);
        scoreAndHealthPanel.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        controlPanel.SetActive(true);
        scoreAndHealthPanel.SetActive(true);

        Time.timeScale = 1f; //starts time ticking in game
    }
    public void Settings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void Restart()
    {
        pauseMenu.SetActive(false);
        restartConfirmMenu.SetActive(true);
    } //only shows confirm menu
    public void ConfirmRestart()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } //actual restart of level
    public void ReturnToStartArea() //from any level, returns to Start Area of the game
    {
        pauseMenu.SetActive(false);
        returnConfirmMenu.SetActive(true);

    }
    public void ConfirmReturn()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(0); //zero is the build index for Start Area
    } //actual return to Start Area
    public void Back() //should exist for each menu after navigating through pause menu
    {
        settingsMenu.SetActive(false);
        restartConfirmMenu.SetActive(false);
        returnConfirmMenu.SetActive(false);
        controlPanel.SetActive(false);

        pauseMenu.SetActive(true);
    }


    //for main menu, to avoid weird behavior, very tedious but quick solution
    public void BackToMain()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

   
}
