using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingEnemy : MonoBehaviour
{
    private GameObject waypoint;
    public GameObject hitEffect;

    private Vector2 WaypointPos;

    public float speed;

    public float attackRange;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {

        waypoint = GameObject.Find("wayPointPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        WaypointPos = new Vector2(waypoint.transform.position.x, waypoint.transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, WaypointPos, speed * Time.deltaTime);

        Collider2D HomingBomb = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);

        if(HomingBomb != null)
        {
            HomingBomb.GetComponent<PlayerMovement>().LifeRadial.fillAmount -= 0.1f;
            HomingBomb.GetComponent<PlayerMovement>().hitByEnemy = true;
            HomingBomb.GetComponent<PlayerMovement>().timeFreeze = true;
            Instantiate(hitEffect, transform.position, transform.rotation);
            Destroy(gameObject);

        }
    }

    public void Die()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if(transform.position == null)
        {
            return;

        }
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
