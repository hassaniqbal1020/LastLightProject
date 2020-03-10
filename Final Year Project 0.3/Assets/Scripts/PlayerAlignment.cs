using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAlignment : MonoBehaviour
{
    public PlayerAttack attackRef;
    public Image Good;
    public Image Bad;
    public Image powerButton;
    public string Alignment;
    bool enable;
    bool buttonEnable;
    float DpadHorizontal;
    float fillAmount;
    public Animator diamondAnim;
    public float posNum;
    int baseDamage;

    // Start is called before the first frame update
    void Start()
    {
        Alignment = "Neutral";
        Good.fillAmount = 0.5f;
        Bad.fillAmount = 0.5f;
        enable = false;
        fillAmount = 0.1f;
        powerButton.enabled = false;
        posNum = 0;
        baseDamage = 5;

    }

    // Update is called once per frame
    void Update()
    {
        DpadHorizontal = Input.GetAxis("XboxDpadX");

        Debug.Log(DpadHorizontal);

        if (enable)
        {
            StartCoroutine(alignmentControl());
            buttonEnable = true;
            powerButton.enabled = true;
        }

        if (!enable)
        {
            buttonEnable = false;
            powerButton.enabled = false;

        }
            


    }

    IEnumerator alignmentControl()
    {
        yield return new WaitForSeconds(0.1f);
        //show UI
        if (DpadHorizontal == 1 && buttonEnable)
        {
            Good.fillAmount += fillAmount;
            Bad.fillAmount -= fillAmount;
            buttonEnable = false;
            enable = false;
            powerButton.enabled = false;
            diamondAnim.SetBool("Used", true);
            posNum += 1;
        }
        else if (DpadHorizontal == -1 && buttonEnable)
        {
            Good.fillAmount -= fillAmount;
            Bad.fillAmount += fillAmount;
            buttonEnable = false;
            enable = false;
            powerButton.enabled = false;
            diamondAnim.SetBool("Used", true);
            attackRef.attackDamage += baseDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("LightOrb"))
        {
            enable = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LightOrb"))
        {
            enable = false;

        }
    }
}
