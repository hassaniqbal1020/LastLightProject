using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public Main mRef;
    public string PlayerState;

    // Start is called before the first frame update
    void Start()
    {
        PlayerState = "inActive";

    }

    // Update is called once per frame
    void Update()
    {
        if(mRef.MainState == "Play")
        {
            PlayerState = "Active";
        }
        else
        {
            PlayerState = "inActive";
        }
    }
}
