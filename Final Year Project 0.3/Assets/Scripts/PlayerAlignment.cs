using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAlignment : MonoBehaviour
{
    public float posNum;
    public float negNum;
    public float detectRange;
    public int PlayerAttackPower;

    public Transform OriginPoint;

    public LayerMask orbLayer;

    public PlayerMovement pRef;
    public DpadController dpadRef;

    public int baseDamage;

    public static PlayerAlignment instance;

    public Image powerButton;

    bool positive;
    bool negative;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

        powerButton = GameObject.FindGameObjectWithTag("PBUI").GetComponent<Image>();

        PlayerAttackPower = 25;
        powerButton.enabled = false;


    }

    // Start is called before the first frame update
    void Start()
    {
        posNum = 0;
        negNum = 0;
        baseDamage = 12;

    }

    // Update is called once per frame
    void Update()
    {
        OriginPoint = GameObject.FindGameObjectWithTag("OriginPoint").GetComponent<Transform>();
        pRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        powerButton = GameObject.FindGameObjectWithTag("PBUI").GetComponent<Image>();
        dpadRef = GameObject.FindGameObjectWithTag("DpadController").GetComponent<DpadController>();

        pRef.AttackPower = PlayerAttackPower;
       
        PowerCoreCollect();

        PositiveAlignment();
        NegativeAlignment();
    }

    void PowerCoreCollect()
    {
        Collider2D[] PowerCores = Physics2D.OverlapCircleAll(OriginPoint.position, detectRange, orbLayer);

        foreach(Collider2D PowerCore in PowerCores)
        {
            powerButton.enabled = true;

            if (dpadRef.IsRight)
            {
                PowerCore.GetComponent<diamondScript>().Collected(true);
                posNum++;
            }
            else if (dpadRef.IsLeft)
            {
                PowerCore.GetComponent<diamondScript>().Collected(true);
                PlayerAttackPower += baseDamage;
                negNum++;
            }

        }
        
        if (PowerCores.Length <= 0)
        {
            powerButton.enabled = false;
        }

    }

    void PositiveAlignment()
    {
        if(posNum >= 1)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        if(posNum >= 2)
        {
            transform.GetChild(2).gameObject.SetActive(false);

        }

        if (posNum >= 3)
        {
            transform.GetChild(4).gameObject.SetActive(false);

        }

        if (posNum >= 4)
        {
            transform.GetChild(6).gameObject.SetActive(false);

        }
    }

    void NegativeAlignment()
    {
        if (negNum >= 1)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }

        if (negNum >= 2)
        {
            transform.GetChild(3).gameObject.SetActive(true);

        }

        if (negNum >= 3)
        {
            transform.GetChild(5).gameObject.SetActive(true);

        }

        if (negNum >= 4)
        {
            transform.GetChild(7).gameObject.SetActive(true);

        }
    }

    private void OnDrawGizmos()
    {
        if(OriginPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(OriginPoint.position, detectRange);
    }

}
