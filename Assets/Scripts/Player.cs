using UnityEngine;
using InControl;
using System.Collections;

public class Player : MonoBehaviour 
{	
	private RaycastHit hitInfo;

	public int PlayerNumber;
	public Camera camera;
	public bool canJump = true;
	public float jumpHeight = 1.0f;
	public LayerMask GroundCheck;
	private bool grounded = false;
	private float tempYRotation;

	private Vector3 velocity;

	#region Horizontal Movement values
	private Vector2 leftStickInput;
	private Vector2 currentHorizVeloc;
	private float maxSpeed = 15f;
	private float maxSpeedChangeGround = 3f;
	private float maxSpeedChangeAir = 0.65f;
	private float deccelerationGround = 0.8f;
	private float deccelerationAir = 0.94f;

	private Vector2 deccelHold;
	private float minDeccel = 0.001f;
	private float maximumDeccel = 10f;

	private Vector3 floorNormal;
	private Vector3 rotatedVelocity;
	#endregion

	#region Jump Values
	private float jumpSpeed = 15f;
	private float jumpSpeedHolder = 1f;
	private float jumpReducer = 0.2f;
	private float ySpeed = 5f;
	private float gravity = 10f;
	private float gravityMax = -10f;
	private float gravityForce = 0.4f;

	private bool inJumpPad = false;
	private float jumpPadStrength = 0f;
	#endregion

	#region Gravity values
	private float initialGravity = 0.4f;
	private float initialGravityCut = 1f;
	private float midGravity = 0.3f;
	private float midGravityCut = -3f;
	private float finalGravity = 0.55f;
	#endregion

	#region Flag stuff
	private bool hasFlag = false;
	public GameObject flagSpot;
	#endregion

	public InputDevice Controller;
    
    public CharacterActions InControlInput;
		 
	void Awake () 
	{
		//rigidbody.freezeRotation = true;
		//rigidbody.useGravity = false;
	}

	void Start()
	{
		#region InControl setup
		InControlInput = new CharacterActions();
        
        InControlInput.Left.AddDefaultBinding(Key.A);
		InControlInput.Right.AddDefaultBinding(Key.D);
		InControlInput.Up.AddDefaultBinding(Key.W);
		InControlInput.Down.AddDefaultBinding(Key.S);
		
		InControlInput.Fire.AddDefaultBinding(Mouse.LeftButton);
		InControlInput.AltFire.AddDefaultBinding(Mouse.RightButton);
		InControlInput.Jump.AddDefaultBinding(Key.Space);
		InControlInput.Interact.AddDefaultBinding(Key.E);
		#endregion
	}

	void Update()
	{
		if(Physics.Raycast(transform.position, Vector3.down, out hitInfo, GetComponent<Collider>().bounds.extents.y + 0.1f, GroundCheck))
		{
			grounded = true;
			floorNormal = hitInfo.normal;
			ySpeed = 0;
			velocity.y = ySpeed;
		}
		else grounded = false;


		#region Jumping
		if(grounded)
		{
			if(InControlInput.Jump.WasPressed)
			{
				Jump ();
			}
		}
		/*else if(jumpTwo && tempNoLand < Time.time)
		{
			if(Controller.Action1.WasPressed)
			{
				Jump ();
				jumpTwo = false;
			}
		}*/
		#endregion
	}
	
	void FixedUpdate () 
	{
		//grounded = Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y + 0.1f, GroundCheck);
		leftStickInput = Controller.LeftStick;
		if(leftStickInput.magnitude > 1) leftStickInput.Normalize();
		leftStickInput = RotateVector ();

		if(grounded)
		{
			currentHorizVeloc -= minMaxDeccel(deccelerationGround);


			currentHorizVeloc += leftStickInput * maxSpeedChangeGround;
			if(currentHorizVeloc.magnitude > maxSpeed)
			{
				currentHorizVeloc = currentHorizVeloc.normalized * maxSpeed;
			}

			velocity.x = currentHorizVeloc.x;
			velocity.z = currentHorizVeloc.y;

			if (canJump && Controller.Action1.WasPressed) 
			{
				GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}

			else
			{				
				GetComponent<Rigidbody>().velocity = velocity;

			}
		}

		else if(!grounded)
		{
			currentHorizVeloc -= minMaxDeccel(deccelerationAir);
			currentHorizVeloc += leftStickInput * maxSpeedChangeAir;
		
			if(currentHorizVeloc.magnitude > maxSpeed)
			{
				currentHorizVeloc = currentHorizVeloc.normalized * maxSpeed;
			}
				
			#region Gravity
			if(ySpeed > initialGravityCut) ySpeed -= initialGravity;
			else if(ySpeed > midGravityCut) ySpeed -= midGravity;
			else ySpeed -= finalGravity;

			velocity.y = ySpeed;
			
			velocity.x = currentHorizVeloc.x;
			velocity.z = currentHorizVeloc.y;

			#endregion

			GetComponent<Rigidbody>().velocity = velocity;
		}

		tempYRotation = camera.transform.eulerAngles.x - Controller.RightStickY.Value;

		if(tempYRotation >= 270)
		{
			tempYRotation = Mathf.Clamp(tempYRotation, 275, 360);
        }

		else if(tempYRotation <= 91)
		{
			tempYRotation = Mathf.Clamp(tempYRotation, -1, 85);
		}

		camera.transform.localEulerAngles = new Vector3(tempYRotation, 0, 0);
		transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + Controller.RightStickX.Value, 0);
	}

	private Vector2 minMaxDeccel(float decelValue)
	{
		deccelHold = currentHorizVeloc - (currentHorizVeloc * decelValue);
		
		if(deccelHold.magnitude > maximumDeccel)
		{
			deccelHold = deccelHold.normalized * maximumDeccel;
		}
		else if(deccelHold.magnitude < minDeccel)
		{
			if(currentHorizVeloc.magnitude > minDeccel) deccelHold = currentHorizVeloc;
			else deccelHold = deccelHold.normalized * minDeccel;
		}

		return deccelHold;
	}

	private Vector2 RotateVector()
	{
		//float angle = Vector3.Angle (new Vector2(camera.transform.forward.x, camera.transform.forward.z), Vector2.up);
		float angle = 360 - transform.rotation.eulerAngles.y;
		Vector2 tempVect;
		tempVect.x = (Mathf.Cos (Mathf.Deg2Rad * angle) * leftStickInput.x) - (Mathf.Sin (Mathf.Deg2Rad * angle) * leftStickInput.y);
		tempVect.y = (Mathf.Cos (Mathf.Deg2Rad * angle) * leftStickInput.y) + (Mathf.Sin (Mathf.Deg2Rad * angle) * leftStickInput.x);
		return tempVect;
	}
	
	private void Jump()
	{
		if(!inJumpPad) jumpSpeedHolder = jumpSpeed;
		else jumpSpeedHolder = jumpPadStrength;
		ySpeed = jumpSpeedHolder;
		grounded = false;
	}
	
	float CalculateJumpVerticalSpeed () 
	{
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}

	public void ApplyHit()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "JumpPad")
		{
			inJumpPad = true;
			//jumpPadStrength = other.GetComponent<JumpPad>().jumpStrength;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "JumpPad") inJumpPad = false;
	}


}
