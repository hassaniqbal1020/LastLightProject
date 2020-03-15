using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class checkpoint : MonoBehaviour
{
    public Vector2 lastPos; // Players last position

    public bool isDead; // Whether player is dead

    public Image hitEffect; // Hit UI

    float freezeTimer; // Freeze game when the player is hit

    // Start is called before the first frame update
    void Start()
    {
        isDead = false; // Setting bool
        hitEffect.enabled = false; // Disabling the UI
        freezeTimer = 1f; // Setting value for how long to freeze the game

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) // What to do if player dies (freeze game and reduce timer)
        {
            freezeTimer -= Time.fixedDeltaTime;
            Time.timeScale = 0;


        }

        if(freezeTimer <= 0) // What to do when timer reaches 0 (reset variables, send player to checkpoint)
        {
            Time.timeScale = 1;
            transform.position = lastPos;
            isDead = false;
            freezeTimer = 1f;
            hitEffect.enabled = false;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint")) // Setting last position when triggering a checkpoint
        {
            lastPos = collision.gameObject.transform.position;

        }

        if (collision.CompareTag("Spike")) // Player dies when touching obstacle
        {
            isDead = true;
            hitEffect.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike") // Player dies when touching obstacle
        {
            isDead = true;
            hitEffect.enabled = true;
        }
    }


}
