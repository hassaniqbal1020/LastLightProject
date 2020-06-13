using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AttackTut : MonoBehaviour
{
    bool enable;
    bool enableB;
    public Image Bbutton;

    // Start is called before the first frame update
    void Start()
    {
        Bbutton.enabled = false;
        enable = false;
        enableB = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(enable && Input.GetButtonDown("Xbox_Y"))
        {
            enableB = true;

        }

        if (enableB)
        {
            Bbutton.enabled = true;


        }else if (!enable)
        {
            Bbutton.enabled = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enable = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enable = false;
            enableB = false;

        }
    }
}
