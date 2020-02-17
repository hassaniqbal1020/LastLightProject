using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public Image Ybutton;
    public Text InteractText;
    bool enable;

    // Start is called before the first frame update
    void Start()
    {
        Ybutton.enabled = false;
        InteractText.enabled = false;
        enable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            Ybutton.enabled = true;
            InteractText.enabled = true;

            if (Input.GetButtonDown("Xbox_Y"))
            {
                enable = false;
            }

        }
        
        if (!enable)
        {
            Ybutton.enabled = false;
            InteractText.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") || collision.CompareTag("Range") || collision.CompareTag("Melee"))
        {
            enable = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable") || collision.CompareTag("Range") || collision.CompareTag("Melee"))
        {
            enable = false;
        }
    }
}
