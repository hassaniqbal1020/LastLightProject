using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pickup : MonoBehaviour
{
    public GameObject pRef;
    public Text WeaponText;
    bool enable;
    bool collected;


    // Start is called before the first frame update
    void Start()
    {
        enable = false;
        WeaponText.enabled = false;
        collected = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (enable && Input.GetButtonDown("Xbox_Y"))
        {
            pRef.GetComponent<PlayerMovement>().rPickup = true;
            pRef.GetComponent<PlayerMovement>().mPickup = true;
            WeaponText.enabled = true;
            collected = true;

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
        if (collision.CompareTag("Player") && enable && collected)
        {
            WeaponText.enabled = false;

            gameObject.SetActive(false);

        }

        if (collision.CompareTag("Player"))
        {
            enable = false;

        }
    }
}
