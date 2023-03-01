using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlockable : MonoBehaviour
{
    public string levelName;
    public int scoreToReach;

    private int levelScore;

    // Start is called before the first frame update
    void Start()
    {
        string getName = "hs" + levelName;
        levelScore = PlayerPrefs.GetInt(getName);

        if (levelScore >= scoreToReach)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
