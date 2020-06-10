using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Dissolver : MonoBehaviour
{
    float fade = 1;

    public bool isDisolving;
    public bool isAttack;

    public GameObject[] materials;
    // Start is called before the first frame update
    void Start()
    {
        isDisolving = false;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject material in materials)
        {

            if (isDisolving)
            {
                fade -= Time.deltaTime;
                material.GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);

                if (fade <= 0)
                {
                    fade = 0f;
                    isDisolving = false;

                }
            }

            if (isAttack)
            {
                material.GetComponent<SpriteRenderer>().material.SetColor("_EnemyColour", Color.red * 2);

            }


        }
    }
}
