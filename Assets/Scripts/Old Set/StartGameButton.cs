using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerControls playerControls;

    [Header("Button Variables")]
    public LayerMask buttonLayer;
    public float buttonRange;
    public Vector3 buttonDefPos; //unpressed position of button;
    public Vector3 buttonPressPos; //pressed position of button;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
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

    private void FixedUpdate()
    {
        ButtonPressed();

        if (!gameManager.isGameStarted)
        {
            gameObject.transform.localPosition = buttonDefPos;
        }
        else
        {
            gameObject.transform.localPosition = buttonPressPos;
        }
    }

    private void ButtonPressed()
    {
        /*Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if(Physics.Raycast(ray, out RaycastHit hitInfo, buttonRange, buttonLayer))
        {
            if(hitInfo.collider.gameObject.layer == this.gameObject.layer)
            {
                if (playerControls.Player.Interact.IsPressed())
                {
                    if (gameManager.isGameStarted)
                    {
                        if (gameManager.isTutorial)
                        {
                            gameManager.EndGame();
                            return;
                        }

                        return;
                    }

                    gameManager.StartGame(); 
                }
            }
        }*/
    }
    
}

