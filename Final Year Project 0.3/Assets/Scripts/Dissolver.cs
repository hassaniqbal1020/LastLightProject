using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Dissolver : MonoBehaviour
{
    float fade = 1;
    float MinusFade;

    public bool isDisolving;
    public bool isAttack;

    public GameObject[] materials;
    // Start is called before the first frame update
    void Start()
    {
        isDisolving = false;
        isAttack = false;
        MinusFade = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject material in materials)
        {

            if (isDisolving)
            {
                fade -= MinusFade;
                material.GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);
                gameObject.layer = default;

                if (fade <= 0)
                {
                    fade = 0f;
                    gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                    isDisolving = false;

                }
            }



        }

    }
}
