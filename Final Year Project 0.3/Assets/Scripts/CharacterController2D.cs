using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	public float JumpForce = 400f; // Jump force of the player	
	const float GroundedRadius = .2f; // Radius of ground collider radius
	const float CeilingRadius = .2f; // Radius of ceiling collider radius
	[Range(0, .3f)] public float MovementSmoothing = .05f; // Movement smoothing 

	public bool AirControl = false; // Whether or not the player can be controlled in the air	
	public bool Grounded; // Is grounded or not   
	private bool FacingRight = true; // Direction player is facing
	private bool canDoubleJump; // Is double jump available
					
	public Transform GroundCheck; // Check if touching ground						
	public Transform CeilingCheck; // Check if touching ceiling	

	public LayerMask WhatIsGround; // Specifying ground layer	

	private Vector3 Velocity = Vector3.zero; // Players velocity

	private Rigidbody2D Rigidbody2D; // Players rigidbody 2D referance

    //public Animator jumpAnim;

    private void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>(); // Setting referance for players rigidbody 2D

	}

    private void Update()
    {
        if(Grounded == true) // Stop jump animation when grounded
        {
            //jumpAnim.SetBool("Land", true);
           // jumpAnim.SetBool("Jump", false);

        }

        if(Grounded == false) // Play jump animation when not grounded
        {
           // jumpAnim.SetBool("Land", false);
           // jumpAnim.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
	{
		bool wasGrounded = Grounded; // Player was grounded
		Grounded = false; // Setting grounded bool


		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround); // Setting collider for when touching the ground

		for (int i = 0; i < colliders.Length; i++) // Running a loop to check if the player is grounded or not
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

			// If the input is moving the player right and the player is facing left
			if (move > 0 && !FacingRight)
			{
				// flip the player
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right
			else if (move < 0 && FacingRight)
			{
				// flip the player
				Flip();
			}
		}
		// If the player should jump
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
