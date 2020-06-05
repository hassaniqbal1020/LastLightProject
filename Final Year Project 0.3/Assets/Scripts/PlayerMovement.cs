using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

    public int AttackPower;

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

    public GameObject mPref;
    public GameObject Boss01;
    public GameObject Room07;
    public GameObject BossUI;

    public Animator playerAnim;

    public Image hitEffect; // Hit UI
    public Image swordIcon;
    public Image GunIcon;
    public GameObject BossHPBarRef;

    public float Speed;
    public float freezeTimer; // Freeze game when the player is hit
    public bool timeFreeze;

    public Sprite armSwordSprite;
    public Sprite armGunSprite;
    public GameObject armSprite;
    public GameObject wayPointPlayer;

    public float posTimer;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        DisJoint = GetComponent<DistanceJoint2D>();
        DisJoint.enabled = false;
        shootRef.SetActive(false);
        SwordRef.SetActive(false);

        GunIcon.enabled = false;
        swordIcon.enabled = false;

        visible = true;

        LifeRadial.fillAmount = 1f;
        hitEffect.enabled = false;
        freezeTimer = 1f; // Setting value for how long to freeze the game
        BossHPBarRef.SetActive(false);
        Boss01.SetActive(false);
        posTimer = 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        Boss01 = GameObject.FindGameObjectWithTag("BossControl");
        Room07 = GameObject.FindGameObjectWithTag("Room07");
        BossUI = GameObject.FindGameObjectWithTag("BossUI").transform.GetChild(0).gameObject;
        wayPointPlayer = GameObject.FindGameObjectWithTag("WayPoint");

        if(posTimer > 0)
        {
            posTimer -= Time.deltaTime;
        }
        if(posTimer <= 0)
        {
            updatePlayerPos();

            posTimer = 0.1f;


        }
        if (!Room07.transform.GetChild(0).gameObject.activeSelf)
        {
            if (BossUI.gameObject.activeSelf)
            {
                BossUI.SetActive(false);

            }

            if (Boss01.transform.GetChild(0).gameObject.activeSelf)
            {
                Boss01.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (timeFreeze)
        {
            freezeTimer -= Time.fixedDeltaTime;
            Time.timeScale = 0;
            hitEffect.enabled = true;
        }

        if (freezeTimer <= 0)
        {
            Time.timeScale = 1;
            hitEffect.enabled = false;
            timeFreeze = false;
            freezeTimer = 1f;
        }

        if (hitByEnemy)
        {
            hitByEnemy = false;
            playerAnim.SetTrigger("isHit");
        }

        if (PlayerState == "Play")
        {
            if(LifeRadial.fillAmount == 0)
            {
                GetComponent<checkpoint>().isDeadNoLife = true;

            }else if(LifeRadial.fillAmount >= 0.1f)
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

            //Item select
            if (InventoryNumber > 2f)
            {
                InventoryNumber = 1f;

            }

            if (InventoryNumber == 2f && mPickup)
            {
                SwordRef.SetActive(true);
                swordIcon.enabled = true;
                armSprite.GetComponent<SpriteRenderer>().sprite = armSwordSprite;

            }
            else
            {
                SwordRef.SetActive(false);
                swordIcon.enabled = false;

            }

            if (InventoryNumber == 1f && rPickup)
            {
                shootRef.SetActive(true);
                armSprite.GetComponent<SpriteRenderer>().sprite = armGunSprite;
                playerAnim.SetBool("isGun", true);
                GunIcon.enabled = true;


            }
            else
            {
                shootRef.SetActive(false);
                playerAnim.SetBool("isGun", false);
                GunIcon.enabled = false;

            }


            if (Input.GetButton("Xbox_Y") && onPlatform == true)
            {
                mPref.GetComponentInChildren<MovingPlatform>().Active = true;

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

    }

    void updatePlayerPos()
    {
        wayPointPlayer.transform.position = transform.position;

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

        if (collision.CompareTag("BossTrig"))
        {
            //Boss01.transform.GetChild(0).gameObject.SetActive(true);
            Room07.transform.GetChild(0).gameObject.SetActive(true);
            Boss01.transform.GetChild(0).gameObject.SetActive(true);

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
