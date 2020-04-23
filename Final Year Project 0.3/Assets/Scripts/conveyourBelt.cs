using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyourBelt : MonoBehaviour
{
    public List<GameObject> objsOnBelt;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        objsOnBelt = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        if (objsOnBelt.Count != 0)
        {
            for (int i = 0; i < objsOnBelt.Count; i++)
            {
                objsOnBelt[i].transform.position += transform.right * (speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            objsOnBelt.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            objsOnBelt.Remove(collision.gameObject);
        }
    }
}
