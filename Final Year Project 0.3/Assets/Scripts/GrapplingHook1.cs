﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook1 : MonoBehaviour
{
    public float MaxDistance; // Max Distance of Hook
    public float DecreaseDistance; // Decrease length of hook 

    public Transform[] Hooks; // Array of all hooks positions within game
    public Transform HookActive; // Position of currently active hook

    public LayerMask mask; // What Raycast can collide with

    public LineRenderer HookLine; // Render hook within game

    public string ActiveState; // State of grappling hook

    DistanceJoint2D PlayerJoint; // Reference to Distance Joint

    Vector3 targetPos; // Target position for hook

    RaycastHit2D hit; // Raycast to detect hook

    // Start is called before the first frame update
    void Start()
    {
        PlayerJoint = GetComponent<DistanceJoint2D>(); // Set DistanceJoint
        PlayerJoint.enabled = false; // Inactive
        HookLine.enabled = false; // Inactive
        ActiveState = "InActive"; // Set inactive state
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform Hook in Hooks) // Go through array of hooks
        {
            Debug.Log(Hook);
            //HookActive = null;

            if (Vector3.Distance(Hook.position, transform.position) < 5) // If any hook is less than specified distance then hook becomes active
            {
                Debug.Log("Yes");
                HookActive = Hook;

            }

        }

        if (Input.GetButtonDown("Xbox_Right_Bumper"))
        {
            Debug.Log("Pressed");

            ActiveState = "Active"; // Active after input
            targetPos = HookActive.transform.position; // Connected target equals active hook position

            targetPos.z = 0; //Set Z to 0

            hit = Physics2D.Raycast(transform.position, targetPos - transform.position, MaxDistance, mask); //Set Raycast origin and direction

            if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) //Raycast collides with something, collides with gameObject Rigidbody2D
            {

                PlayerJoint.enabled = true; //Active
                PlayerJoint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>(); //Set ConnectedBody to the Rigidbady2D hit by Raycast
                PlayerJoint.distance = Vector2.Distance(transform.position, hit.point); //Set distance between player and Rigidbody2D
                PlayerJoint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y); // Point connects to the edge of hook rather than the middle

                // Enabling line renderer and setting position
                HookLine.enabled = true; 
                HookLine.SetPosition(0, transform.position);
                HookLine.SetPosition(1, hit.point);


            }

        }

        if (Input.GetButton("Xbox_Right_Bumper")) // Enabling line renderer and setting position
        {
            HookLine.SetPosition(0, transform.position);

        }

        if (Input.GetButtonUp("Xbox_Right_Bumper")) // Disabling line renderer and setting position
        {
            PlayerJoint.enabled = false;
            HookLine.enabled = false;
            ActiveState = "InActive";

        }

        if (PlayerJoint.distance > 0.7f) // Set minimum distance for hook length to begin decreasing
        {
            PlayerJoint.distance -= DecreaseDistance * Time.deltaTime;

        }
        else
        {
            PlayerJoint.distance -= 0; // Stop decrreasing when reached minimum

        }

        Debug.DrawRay(transform.position, targetPos - transform.position); // Raycast visible within scene view


    }
}
