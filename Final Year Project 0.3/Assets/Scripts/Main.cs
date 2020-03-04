using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public string MainState;
    public PlayerMovement pRef;


    // Start is called before the first frame update
    void Start()
    {
        MainState = "Play";
        pRef.PlayerState = "Play";

    }

    // Update is called once per frame
    void Update()
    {

    }
}
