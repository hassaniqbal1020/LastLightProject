using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pickup : MonoBehaviour
{
    public Main mRef;
    public Text WeaponText;
    public GameObject Effect;
    bool enable;
    bool collected;

    void Awake()
    {
        enable = false;
        WeaponText.enabled = false;
        collected = false;
        mRef = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enable && Input.GetButtonDown("Xbox_Y"))
        {
            mRef.hasWeapons = true;
            WeaponText.enabled = true;
            collected = true;
            Instantiate(Effect, transform.position, transform.rotation);
        }

        if (mRef.hasWeapons)
        {
            gameObject.SetActive(false);
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
