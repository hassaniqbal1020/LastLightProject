using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyBomb : MonoBehaviour
{
    public float Timer;
    bool TimerSwitch;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        TimerSwitch = false;
        Timer = 1f;
        rb = GetComponent<Rigidbody2D>();
        GetComponent<BombScript>().BombTimer = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerSwitch)
        {
            Timer -= Time.deltaTime;

        }

        if(Timer <= 0)
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            GetComponent<BombScript>().Activate = true;
            Timer = 1f;
            TimerSwitch = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
            TimerSwitch = true;
        }
    }
}
