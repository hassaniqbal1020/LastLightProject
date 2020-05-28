using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RoomScript : MonoBehaviour
{
    public GameObject VirtualCamera;
    public GameObject RoomItems;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" & !collision.isTrigger)
        {
            VirtualCamera.SetActive(true);
            RoomItems.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" & !collision.isTrigger)
        {
            VirtualCamera.SetActive(false);
            RoomItems.SetActive(false);

        }
    }
}
