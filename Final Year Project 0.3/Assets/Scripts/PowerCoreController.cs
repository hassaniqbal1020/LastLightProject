using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCoreController : MonoBehaviour
{
    private static PowerCoreController instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

}
