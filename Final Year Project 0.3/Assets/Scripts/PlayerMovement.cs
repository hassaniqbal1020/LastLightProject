using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float horizontal = 0f;
    bool jump = false;
    bool crouch = false;
    bool dash = false;
    bool onPlatform = false;
    bool onWater = false;
    public bool rPickup;
    public bool mPickup;

    public float InventoryNumber = 1f;
    string GrapHookActive;
    public float DashTimer = 0.5f;
    public string DashState;


    DistanceJoint2D DisJoint;

    private Rigidbody2D rb;
    public float ThrustForce;

    public GameObject mouseRef;
    public GameObject shootRef;
    //public GameObject enemyRef;
    public GameObject shieldRef;
    public GameObject SwordRef;


    public MovingPlatform mPref;

    //public Animator playerAnim;


    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        DisJoint = GetComponent<DistanceJoint2D>();
        DisJoint.enabled = false;
        shootRef.SetActive(false);
        SwordRef.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GrapplingHook1>().ActiveState == "InActive")
        {
            GrapHookActive = "InActive";

        }

        if (GetComponent<GrapplingHook1>().ActiveState == "Active")
        {
            GrapHookActive = "Active";

        }
        //Horizontal Movement
        horizontal = Input.GetAxisRaw("XboxLeftStickX") * Speed;

        if (horizontal >= 0 || horizontal <= 0)
        {
            //playerAnim.SetFloat("Speed", 1f);


        }

        if (horizontal == 0)
        {
            //playerAnim.SetFloat("Speed", 0);

        }

        //Player Jump - Double Jump/Super Jump
        if (Input.GetButtonDown("Xbox_A") && onPlatform == false && onWater == false)
        {
            jump = true;

        }

        if (jump == true)
        {
            //playerAnim.SetBool("Jump", true);



        }

        //Player Crouch
        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;

        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;

        }

        //Player Dash
        if (Input.GetButtonDown("Xbox_X") && DashState == "nDash" && horizontal != 0)
        {
            dash = true;
            DashState = "Dash";
        }

        if (DashState == "Dash")
        {
            DashTimer -= Time.deltaTime;
        }

        if (DashTimer <= 0)
        {
            DashTimer = 0.5f;
            DashState = "nDash";


        }

        if (Input.GetButtonDown("Xbox_Left_Bumper"))
        {
            InventoryNumber += 1f;


        }

        if (Input.GetAxisRaw("XboxLeftTrigger") == 1)
        {
            InventoryNumber = 0f;

        }


        //Item select
        if (InventoryNumber > 2f)
        {
            InventoryNumber = 1f;

        }

        //print(horizontal);

        if (InventoryNumber == 2f && mPickup)
        {
            SwordRef.SetActive(true);

        }
        else
        {
            SwordRef.SetActive(false);

        }

        if (InventoryNumber == 1f && rPickup)
        {
            shootRef.SetActive(true);

        }
        else
        {
            shootRef.SetActive(false);

        }

        if (InventoryNumber == 0f)
        {
            shieldRef.SetActive(true);

        }
        else
        {
            shieldRef.SetActive(false);

        }


        if (Input.GetButton("Xbox_A") && onPlatform == true)
        {
            mPref.Active = true;

        }


    }

    void FixedUpdate()
    {
        //Reference to controller to Move/Crouch/Jump
        controller.Move(horizontal * Time.fixedDeltaTime, jump);
        jump = false;

        //Dash Code - hold direction for dash
        if (dash == true && DashState == "Dash")
        {

            if (horizontal > 0)
            {
                rb.AddForce(transform.right * ThrustForce, ForceMode2D.Impulse);
                dash = false;


            }
            else if (horizontal < 0)
            {
                rb.AddForce(transform.right * ThrustForce, ForceMode2D.Impulse);
                dash = false;

            }

        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "mPlatform")
        {
            transform.parent = collision.gameObject.transform;

            onPlatform = true;


        }


    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mPlatform")
        {
            transform.parent = null;

            onPlatform = false;
            

        }

        if (collision.gameObject.tag == "Waterfall")
        {
            onWater = false;
        }
            
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Waterfall")
        {
            rb.velocity = new Vector3(-20, 0, 0);
            onWater = true;


        }
    }

}
