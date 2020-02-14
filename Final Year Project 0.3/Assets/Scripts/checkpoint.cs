using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public Vector2 lastPos;
    public bool isDead;
    public float dTimer;

    // Start is called before the first frame update
    void Start()
    {
        dTimer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            dTimer -= Time.deltaTime;
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        if(dTimer <= 0)
        {
            gameObject.transform.position = lastPos;
            dTimer = 0.5f;
            GetComponent<PlayerStates>().isDead = false;
            isDead = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            lastPos = collision.gameObject.transform.position;

        }
    }
}
