using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeDialogue : MonoBehaviour
{
    PlayerControls playerControls;

    public GameObject controlsPanel;

    public Animator dialogueAnimator;
    int dialogueIndex;
    public Text dialogueText;
    public float textSpeed;

    [TextArea]
    public string[] dialogue;

    bool inDialogue = false;

    //Player Controls initialization
    private void Awake()
    {
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

    void Start()
    {
        if (PlayerPrefs.GetInt("playedOnce") == 0)
        {
            Debug.Log("Has not been played before");
            StartWelcome();
        }
        else
        {
            Debug.Log("Already played once");
        }
    }

    private void Update()
    {
        if (inDialogue)
        {
            if (playerControls.Player.Continue.WasReleasedThisFrame())
            {
                if (dialogueText.text == dialogue[dialogueIndex])
                {
                    NextLine(dialogue);
                }
                else
                {
                    StopAllCoroutines();

                    dialogueText.text = dialogue[dialogueIndex];
                }
            }
        }
    }

    void StartWelcome()
    {
        controlsPanel.SetActive(false);

        dialogueAnimator.SetTrigger("ShowDialogue");
        inDialogue = true;

        StartDialogue();
    }
    void EndWelcome()
    {
        dialogueAnimator.SetTrigger("HideDialogue");
        inDialogue = false;

        controlsPanel.SetActive(true);

        PlayerPrefs.SetInt("playedOnce", 1);
    }

    public void StartDialogue() //start of dialogue system
    {
        dialogueText.text = "";
        dialogueIndex = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine() //a method to add characters to the dialogue one-by-one, animation of sorts
    {
        foreach (char c in dialogue[dialogueIndex].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void NextLine(string[] dialogue) //to begin next line in dialogue system
    {
        if (dialogueIndex < dialogue.Length - 1)
        {
            dialogueIndex += 1;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            EndWelcome();
        }
    }
}
