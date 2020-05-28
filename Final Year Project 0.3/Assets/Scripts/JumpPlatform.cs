using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public bool on;

    public GameObject pRef;

    public Sprite OnSprite;
    public Sprite OffSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(on);

        pRef = GameObject.FindGameObjectWithTag("Player");

        if(Input.GetButtonDown("Xbox_A") && pRef.GetComponent<CharacterController2D>().canDoubleJump)
        {
            Flip();
        }

        if (on)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = OnSprite;

        }else if (!on)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = OffSprite;


        }
    }

    public void Flip()
    {
        on = !on;
    }
}
