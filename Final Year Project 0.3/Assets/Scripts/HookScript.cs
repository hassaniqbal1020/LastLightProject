using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public Transform playerTransform; // Position of player

    public float detectRange = 0.5f; // Radius of detection box

    public LayerMask Player; // Layer for detection box to take effect

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hookRange = Physics2D.OverlapCircle(transform.position, detectRange, Player); // Detection box, whether player is within range

        // Change colour of hook if player is within range
        if(hookRange != null)
        {
            GetComponent<SpriteRenderer>().color = Color.red;

        }else if(hookRange == null)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }



    }

    private void OnDrawGizmosSelected()
    {
        // Display detection box within scene view
        if (transform.position == null)
        {
            return;

        }
        Gizmos.DrawWireSphere(transform.position, detectRange);

    }
}
