using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook1 : MonoBehaviour
{
    DistanceJoint2D PlayerJoint; //reference to Distance Joint
    Vector3 targetPos; //Position
    RaycastHit2D hit;
    public float MaxDistance; //Max Distance of Hook
    public LayerMask mask; //What Raycast can collide with
    public LineRenderer HookLine;
    public float DecreaseDistance;
    public string ActiveState;
    public Transform[] Hooks;
    public Transform HookActive;

    // Start is called before the first frame update
    void Start()
    {
        PlayerJoint = GetComponent<DistanceJoint2D>(); //Set DistanceJoint
        PlayerJoint.enabled = false; //Inactive
        HookLine.enabled = false;
        ActiveState = "InActive";
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform Hook in Hooks)
        {
            Debug.Log(Hook);
            //HookActive = null;

            if (Vector3.Distance(Hook.position, transform.position) < 5)
            {
                Debug.Log("Yes");
                HookActive = Hook;

            }

        }

        if (Input.GetButtonDown("Xbox_Right_Bumper"))
        {
            Debug.Log("Pressed");

            ActiveState = "Active";
            targetPos = HookActive.transform.position;

            targetPos.z = 0; //Set Z to 0

            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, MaxDistance, mask); //Set Raycast origin and direction

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) //Raycast collides with something, collides with gameObject Rigidbody2D
            {

                PlayerJoint.enabled = true; //Active
                PlayerJoint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>(); //Set ConnectedBody to the Rigidbady2D hit by Raycast
                PlayerJoint.distance = Vector2.Distance(transform.position, hit.point); //Set distance between player and Rigidbody2D
                PlayerJoint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);

                HookLine.enabled = true;
                HookLine.SetPosition(0, transform.position);
                HookLine.SetPosition(1, hit.point);


            }

        }

        if (Input.GetButton("Xbox_Right_Bumper"))
        {
            HookLine.SetPosition(0, transform.position);

        }

        if (Input.GetButtonUp("Xbox_Right_Bumper"))
        {
            PlayerJoint.enabled = false;
            HookLine.enabled = false;
            ActiveState = "InActive";

        }

        if (PlayerJoint.distance > 0.7f)
        {
            PlayerJoint.distance -= DecreaseDistance * Time.deltaTime;

        }
        else
        {
            PlayerJoint.distance -= 0;

        }

        Debug.DrawRay(transform.position, targetPos - transform.position);


    }
}
