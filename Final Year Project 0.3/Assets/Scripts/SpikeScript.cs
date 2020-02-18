using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    bool dead;
    public GameObject deathEffect;


    // Start is called before the first frame update
    void Start()
    {
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            StartCoroutine(Die());

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            dead = true;

        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.01f);
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
