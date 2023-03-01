using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    public GameObject targetObject;
    public GameObject hideObject;

    private void Update()
    {
        if (targetObject.activeInHierarchy)
        {
            hideObject.SetActive(false);
        }

        if (!targetObject.activeInHierarchy)
        {
            hideObject.SetActive(true);
        }
    }
}
