using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashTut : MonoBehaviour
{
    public Image DashButton;
    public Image Lstick;
    bool enable;
    bool Enablebutton;

    // Start is called before the first frame update
    void Start()
    {
        enable = false;
        Enablebutton = false;
        DashButton.enabled = false;
        Lstick.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (enable && Input.GetButtonDown("Xbox_Y"))
        {
            Enablebutton = true;

        }

        if (Enablebutton)
        {
            DashButton.enabled = true;
            Lstick.enabled = true;

        }

        if (!Enablebutton)
        {
            DashButton.enabled = false;
            Lstick.enabled = false;

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
            Enablebutton = false;


        }
    }
}
