using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RoomScript : MonoBehaviour
{
    public GameObject VirtualCamera;
    public GameObject RoomItems;

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
