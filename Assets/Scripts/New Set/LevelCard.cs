using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelCard : MonoBehaviour
{
    public Animator levelCard;
    public Text levelText;

    public float hideCardTime = 1.5f;
    public void Start()
    {
        levelText.text = SceneManager.GetActiveScene().name;

        StartCoroutine(HideLevel());
    }

    IEnumerator HideLevel()
    {
        yield return new WaitForSeconds(hideCardTime);

        levelCard.SetTrigger("hideLevel");
    }

}
