using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    //Level Loading
    public void returnToMainMenu()
    {
        StartCoroutine(LoadLevel("Main Menu"));
    }
    public void PlayGame()
    {
        StartCoroutine(LoadLevel("The Park"));
    }
    public void ReloadLevel()
    {
        string thisLevel = SceneManager.GetActiveScene().name;

        StartCoroutine(LoadLevel(thisLevel));
    } 


    //Coroutine for transition animation and level loading
    IEnumerator LoadLevel(string level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }

    //Pause Menu
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
    }

    /*public void QuitGame()
    {
        Application.Quit();
    }*/
}
