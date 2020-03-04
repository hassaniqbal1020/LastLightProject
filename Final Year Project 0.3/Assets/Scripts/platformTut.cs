using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class platformTut : MonoBehaviour
{
    public Image Ybutton;
    [SerializeField] bool enable;

    // Start is called before the first frame update
    void Start()
    {
        Ybutton.enabled = false;
        enable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            Ybutton.enabled = true;
            Debug.Log("enable");
        }
        
        if (!enable)
        {
            Ybutton.enabled = false;

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
        }
    }
}
