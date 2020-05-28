using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFunction : MonoBehaviour
{
    public GameObject Bomb;
    public GameObject spwnPoint;


    void LoadBomb()
    {
        if(transform.GetChild(8).gameObject.GetComponent<Boss01>().BossHP <= 200)
        {
            Instantiate(Bomb, spwnPoint.transform.position, spwnPoint.transform.rotation);

        }
    }

}
