using UnityEngine;
using System.Collections;

public class PlatformCharacterController : MonoBehaviour {
	
	private CharacterMotor motor;
	private PhysicsCharacterMotor phyCharMotor;
	public float walkMultiplier = 0.5f;
	public bool defaultIsWalk = false;
	private Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
		phyCharMotor = GetComponent<PhysicsCharacterMotor>();
		motor = GetComponent(typeof(CharacterMotor)) as CharacterMotor;
		if (motor==null) Debug.Log("Motor is null!!");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 ) {
			phyCharMotor.useCentricGravity = true;
			anim.SetBool("IsMoving", true);
		}
		else {
			anim.SetBool("IsMoving", false);
			anim.SetInteger("Idle_Index", Random.Range(0,3));
			if(phyCharMotor.grounded)
				phyCharMotor.useCentricGravity = false;

		}
		// Get input vector from kayboard or analog stick and make it length 1 at most
	
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		if (directionVector.magnitude>1) directionVector = directionVector.normalized;
		directionVector = directionVector.normalized * Mathf.Pow(directionVector.magnitude, 2);
		
		// Rotate input vector into camera space so up is camera's up and right is camera's right
		directionVector = Camera.main.transform.rotation * directionVector;
		
		// Rotate input vector to be perpendicular to character's up vector
		Quaternion camToCharacterSpace = Quaternion.FromToRotation(Camera.main.transform.forward*-1, transform.up);
		directionVector = (camToCharacterSpace * directionVector);
		
		// Make input vector relative to Character's own orientation
		directionVector = Quaternion.Inverse(transform.rotation) * directionVector;
		
		if (walkMultiplier!=1) {
			if ( (Input.GetKey("left shift") || Input.GetKey("right shift")) != defaultIsWalk ) {
				directionVector *= walkMultiplier;
			}
		}
		
		// Apply direction
		motor.desiredMovementDirection = directionVector;
	}
}
