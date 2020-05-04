using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAlignment : MonoBehaviour
{
    public Image powerButton;

    public float posNum;
    float DpadHorizontal;

    bool enable;
    bool buttonEnable;

    public AttackFunction attackRef;

    public string Alignment;

    public Animator diamondAnim;

    int baseDamage;

    // Start is called before the first frame update
    void Start()
    {
        Alignment = "Neutral";
        enable = false;
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
            buttonEnable = false;
            enable = false;
            powerButton.enabled = false;
            diamondAnim.SetBool("Used", true);
            posNum += 1;
        }
        else if (DpadHorizontal == -1 && buttonEnable)
        {
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
