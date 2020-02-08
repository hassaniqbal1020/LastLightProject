using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public float JumpForce = 400f;										
	[Range(0, .3f)] public float MovementSmoothing = .05f;	
	public bool AirControl = false;							
	public LayerMask WhatIsGround;							
	public Transform GroundCheck;							
	public Transform CeilingCheck;										

	const float GroundedRadius = .2f; 
	private bool Grounded;            
	const float CeilingRadius = .2f; 
	private Rigidbody2D Rigidbody2D;
	private bool FacingRight = true;  
	private Vector3 Velocity = Vector3.zero;

    //public Animator jumpAnim;

    //public float jumpsLeft = 2f;

    private bool canDoubleJump;


    private void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>();

	}

    private void Update()
    {
        if(Grounded == true)
        {
            //jumpAnim.SetBool("Land", true);
           // jumpAnim.SetBool("Jump", false);

        }

        if(Grounded == false)
        {
           // jumpAnim.SetBool("Land", false);
           // jumpAnim.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
	{
		bool wasGrounded = Grounded;
		Grounded = false;


		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
				
					
			}
		}
	}


	public void Move(float move, bool jump)
	{

		//only control the player if grounded or airControl is turned on
		if (Grounded || AirControl)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref Velocity, MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (jump)
		{
            if (Grounded)
            {
                // Add a vertical force to the player
                Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
                canDoubleJump = true;
                Grounded = false;
                Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);

            }
            else
            {
                if (canDoubleJump)
                {
                    canDoubleJump = false;
                    Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
                    Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
                }
            }


		} 
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		//Quaternion theRotation = transform.rotation;
		//theRotation.y *= -180;
		transform.Rotate (0,180,0);


	}
}
