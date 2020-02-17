using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JumpTutorial : MonoBehaviour
{
    public Image JumpTut;
    [SerializeField]bool enable;
    bool enableJumpText;


    // Start is called before the first frame update
    void Start()
    {
        JumpTut.enabled = false;
        enable = false;
        enableJumpText = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Xbox_Y") && enable)
        {
            enableJumpText = true;

            
        }

        if (enableJumpText)
        {
            JumpTut.enabled = true;

        }else if (!enableJumpText)
        {
            JumpTut.enabled = false;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            enable = true;
        }

    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enable = false;
            enableJumpText = false;


        }
    }
}
