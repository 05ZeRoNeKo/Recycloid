using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Waypoint : MonoBehaviour
{
    public Animator transition;

    public string levelName;
    public float transitionTime = 1f;

    private void OnTriggerEnter(Collider other)
    {
        LoadScene();
    }

    public void LoadScene()
    {
        StartCoroutine(SceneSelect(levelName));
    }
    IEnumerator SceneSelect(string level)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(level);
    }
}
