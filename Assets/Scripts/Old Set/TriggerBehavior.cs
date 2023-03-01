using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerBehavior : MonoBehaviour
{
    public GameObject disableTarget; //refers to object to be deactivated
    public GameObject enableTarget; //refers to object to be activated

    [Space]
    [SerializeField]
    private GameObject controlPanel;
    [SerializeField]
    private GameObject dialogueUI; //dialogue box
    [SerializeField]
    private Text dialogueText; //text in dialogue box
    [SerializeField]
    private string[] dialogue; //array of dialogue for each trigger
    public float textSpeed; //speed of which each character in a dialogue is added

    public int index = 0;

    private PlayerControls playerControls;
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

    private void Update() //to check touch input and manage next line
    {
        if (playerControls.Player.Continue.WasReleasedThisFrame())
        {
            if (dialogueUI.activeInHierarchy)
            {
                if (dialogueText.text == dialogue[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogue[index];
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other) // to trigger start of dialogue system
    {
        if (other.CompareTag("Player"))
        {
            controlPanel.SetActive(false);

            dialogueText.text = string.Empty;
            dialogueUI.SetActive(true);
            StartDialogue();
        }
    }
    private void StartDialogue() //start of dialogue system
    {
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine() //a method to add characters to the dialogue one-by-one, animation of sorts
    {
        foreach(char c in dialogue[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    private void NextLine() //to begin next line in dialogue system
    {
        if(index < dialogue.Length - 1)
        {
            index += 1;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            disableTarget.SetActive(false);
            enableTarget.SetActive(true);
            dialogueUI.SetActive(false);
            controlPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
