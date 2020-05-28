using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public string MainState;
    public PlayerMovement pRef;

    private static Main instance;
    public Vector2 LastPos;

    public bool hasWeapons;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

        }
        else
        {
            Destroy(gameObject);
        }


        MainState = "Play";
        pRef.PlayerState = "Play";
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        pRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        

        if (hasWeapons)
        {
            pRef.mPickup = true;
            pRef.rPickup = true;

        }
    }
}
