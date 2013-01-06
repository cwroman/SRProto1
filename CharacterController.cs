using UnityEngine;
using System.Collections;


// Require a character controller to be attached to the same game object
[RequireComponent (typeof(CharacterMotor))]
[AddComponentMenu ("Character/FPS Input Controller")]

public class CharacterController : MonoBehaviour
{
	public float speedModifier = 1.0f;
	public float jumpModifier = 5.0f;
	public float maxJumpVelocity = 5.0f;
	
	private CharacterController controller;

	private Rigidbody rb;
	// Use this for initialization
	void Start ()
	{
		
		rb = GetComponent<Rigidbody>();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		bool jumping = false;
		if (Input.GetAxis("Jump") > 0)
		{
			jumping = true;
			rb.useGravity = false;
		}
		else
		{
			rb.useGravity = true;	
		}
		
		// Get the input vector from kayboard or analog stick
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		
		if (directionVector != Vector3.zero) {
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min(1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
			rb.MovePosition(rb.position + directionVector * Time.deltaTime * speedModifier);
		}
		
		if (jumping)
		{
			float jumpSpeed = Mathf.Min(maxJumpVelocity, rb.velocity.y + jumpModifier * Time.deltaTime);
			rb.velocity = new Vector3(0, jumpSpeed, 0);
		}
		
		
		// Apply the direction to the CharacterMotor
		//motor.inputMoveDirection = transform.rotation * directionVector;
		//motor.inputJump = Input.GetButton("Jump");
	}
}