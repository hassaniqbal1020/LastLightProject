using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    public Transform playerTransform;
    public float detectRange = 0.5f;
    public LayerMask Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D hookRange = Physics2D.OverlapCircle(transform.position, detectRange, Player);

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
        if (transform.position == null)
        {
            return;

        }
        Gizmos.DrawWireSphere(transform.position, detectRange);

    }
}
