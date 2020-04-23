using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;

    public float horizontal = 0f;
    bool jump = false;
    bool dash = false;
    bool onPlatform = false;
    public bool hitByEnemy = false;
    public bool rPickup;
    public bool mPickup;
    public bool visible;

    public float InventoryNumber = 1f;
    string GrapHookActive;
    public float DashTimer = 0.5f;
    public string DashState;

    public Image joyStickTut;
    public Image LifeRadial;

    DistanceJoint2D DisJoint;

    private Rigidbody2D rb;
    public float ThrustForce;
    public float hitForce;

    public GameObject shootRef;
    //public GameObject enemyRef;
    public GameObject shieldRef;
    public GameObject SwordRef;

    public string PlayerState;

    public MovingPlatform mPref;

    public Animator playerAnim;


    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        DisJoint = GetComponent<DistanceJoint2D>();
        DisJoint.enabled = false;
        shootRef.SetActive(false);
        SwordRef.SetActive(false);

        visible = true;

        LifeRadial.fillAmount = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
      if(PlayerState == "Play")
        {
            if(LifeRadial.fillAmount == 0)
            {
                GetComponent<checkpoint>().isDeadNoLife = true;

            }else if(LifeRadial.fillAmount >= 1)
            {
                GetComponent<checkpoint>().isDeadNoLife = false;
            }

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


            if (horizontal > 0 && controller.Grounded || horizontal < 0 && controller.Grounded)
            {
                joyStickTut.enabled = false;
                playerAnim.SetBool("isRunning", true);


            }

            if(horizontal == 0 && controller.Grounded)
            {
                playerAnim.SetBool("isRunning", false);
            }

            //Player Jump - Double Jump/Super Jump
            if (Input.GetButtonDown("Xbox_A") && onPlatform == false )
            {
                if (controller.canDoubleJump)
                {
                    playerAnim.SetTrigger("LiftOff");
                }
                
                jump = true;
            }

            if (!controller.Grounded && rb.velocity.y > 0.1f)
            {
                playerAnim.SetBool("isJumping", true);

            }
            else if (controller.Grounded)
            {
                playerAnim.SetBool("isJumping", false);
                playerAnim.SetBool("isFalling", false);

            }
            else if(!controller.Grounded && rb.velocity.y < 0.1f)
            {
                playerAnim.SetBool("isFalling", true);
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


            if (Input.GetButton("Xbox_Y") && onPlatform == true)
            {
                mPref.Active = true;

            }
      }


    }

    void FixedUpdate()
    {
        //Reference to controller to Move/Jump
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

        if (hitByEnemy)
        {
            rb.AddForce(transform.right * -hitForce, ForceMode2D.Impulse);
            transform.position += new Vector3(0, 0.1f, 0);
            hitByEnemy = false;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Steam"))
        {
            visible = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Steam"))
        {
            visible = true;


        }
    }

}
