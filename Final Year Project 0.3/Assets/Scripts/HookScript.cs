using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public Transform playerTransform;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player") && Vector3.Distance(playerTransform.position, transform.position) < 4)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (collision.gameObject.CompareTag("Player") && Vector3.Distance(playerTransform.position, transform.position) > 4)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Vector3.Distance(playerTransform.position, transform.position) > 4)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }

        if (collision.gameObject.CompareTag("Player") && Vector3.Distance(playerTransform.position, transform.position) < 4)
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
}
