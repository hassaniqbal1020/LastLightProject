using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class checkpoint : MonoBehaviour
{
    public bool isDead; // Whether player is dead

    public bool isDeadNoLife;

    public Image hitEffect; // Hit UI

    [SerializeField]float freezeTimer; // Freeze game when the player is hit

    public Main mRef;



    // Start is called before the first frame update
    void Start()
    {
        isDead = false; // Setting bool
        hitEffect.enabled = false; // Disabling the UI
        freezeTimer = 1.5f; // Setting value for how long to freeze the game
        mRef = GameObject.FindGameObjectWithTag("Main").GetComponent<Main>();
        transform.position = mRef.LastPos;

    }

    // Update is called once per frame
    void Update()
    {


        if (isDead || isDeadNoLife) // What to do if player dies (freeze game and reduce timer)
        {
            freezeTimer -= Time.fixedDeltaTime;
            Time.timeScale = 0;
            hitEffect.enabled = true;

        }

        if (freezeTimer <= 0) // What to do when timer reaches 0 (reset variables, send player to checkpoint)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            isDead = false;
            freezeTimer = 1.5f;
            hitEffect.enabled = false;

        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint")) // Setting last position when triggering a checkpoint
        {
            mRef.LastPos = collision.gameObject.transform.position;

        }

        if (collision.CompareTag("Spike")) // Player dies when touching obstacle
        {
            isDead = true;
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike") // Player dies when touching obstacle
        {
            isDead = true;

        }
    }


}
