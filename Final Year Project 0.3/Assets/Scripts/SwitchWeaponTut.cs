using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchWeaponTut : MonoBehaviour
{
    bool enable;
    bool TextEnable;
    public Image RBimage;


    // Start is called before the first frame update
    void Start()
    {
        enable = false;
        TextEnable = false;
        RBimage.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(enable && Input.GetButtonDown("Xbox_Y"))
        {
            TextEnable = true;

        }

        if (TextEnable)
        {
            RBimage.enabled = true;

        }else if (!TextEnable)
        {
            RBimage.enabled = false;

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
            TextEnable = false;

        }
    }
}
