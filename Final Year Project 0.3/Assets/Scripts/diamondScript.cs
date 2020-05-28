using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diamondScript : MonoBehaviour
{

    public void Collected(bool hasCollected)
    {
        gameObject.SetActive(false);
    }
}
