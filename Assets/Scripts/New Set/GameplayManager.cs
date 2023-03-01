using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameplayManager : MonoBehaviour
{
    public bool isLevelStarted;

    private LevelTrigger levelTrigger;
    private ScoreManager scoreManager;

    public AudioSource idleMusic;
    public AudioSource gameplayMusic;
    private void Awake()
    {
        isLevelStarted = false;
        levelTrigger = FindObjectOfType<LevelTrigger>();
        scoreManager = FindObjectOfType<ScoreManager>();

        idleMusic.Play();
    }
    public void levelStart()
    {
        isLevelStarted = true;
        idleMusic.Stop();
        gameplayMusic.Play();
        
    }
    public void levelEnd()
    {
        isLevelStarted = false;
        gameplayMusic.Stop();
        idleMusic.Play();

        try //destroys all spawned object once level ends
        {
            Rigidbody[] currentTrash = FindObjectsOfType<Rigidbody>();

            foreach (Rigidbody rb in currentTrash)
            {
                if (rb.gameObject.layer == 6)
                {
                    Destroy(rb.gameObject);
                }
            }
        }
        catch
        {
            return;
        }

        //levelTrigger.gameObject.SetActive(true);
        scoreManager.ShowFinalScore();
    }
}
