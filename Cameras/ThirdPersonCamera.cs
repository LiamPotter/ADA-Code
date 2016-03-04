	using UnityEngine;
	using System.Collections;

	/// <summary>
	/// Camera Script
	/// This Script Controls the different states of the camera;
	/// First Person, Behind The Back, Free camera and Targeting Camera
	/// Uses A Parent Rig to rotate the camera For certain cases, if you didnt use this you will be getting conflict between two camera states.
	/// Camera states is controlled by a case statement
	/// 
	/// Author; Chris Osmond
	/// 
	/// version; 1.0
//	/// </summary>
	struct CameraPosition
	{
		private Vector3 position;

		private Transform xForm;

		public Vector3 Position
		{
			get	{return position;}
			set	{ position = value;}
		}
		public Transform XForm
		{
			get	{return xForm;}
			set	{ xForm = value;}
		}

		public void Init(string camName, Vector3 pos, Transform transform, Transform parent)
		{
			position = pos;
			xForm = transform;
			xForm.name = camName;
			xForm.parent = parent;
			xForm.localPosition = Vector3.zero;
			xForm.localPosition = position;


		}

}


	public class ThirdPersonCamera : MonoBehaviour {
		//<Positioning camera>



		[SerializeField]
		private float distanceAway;
		[SerializeField]
		private float distanceUp;
		//</Positioning camera>

		//Camera rig...
		[SerializeField]
		private Transform parentRig;


		//Follow target for camera 
		[SerializeField]
		private Transform followXForm;

		//<First Person Variables>
		private float xAxisRot = 0.0f;
		private float lookWeight;
		[SerializeField]
		private float firstPersonLookSpeed =2.5f;
		public float fPSRotationDegreePerSecond = 120f;
		[SerializeField]
		private Vector2 firstPersonXAxisClamp = new Vector2(-70.0f, 90.0f);

		//</First Person Variables>

		//<Free Camera Mode Variables>

		//ZOOM IN AND OUT VARIABLED
		[SerializeField]
		private float distanceAwayMultiplyer = 1.5f;
		[SerializeField]
		private float distanceUpMultiplyer = 5f;
		[SerializeField]
		private Vector2 camMinDistFromChar = new Vector2(1f, -0.5f);
		private Vector2 rightStickPrevFrame = Vector2.zero;

		[SerializeField]
		private float freeThreshold = -0.1f;

		[SerializeField]
		private float rightStickThreshold = 0.1f;
		[SerializeField]
		private float freeRotationDegreePerSecond = -10f;
		private Vector3 saveRigToGoal;
		private float dstAwayFree;
		private float dstUpFree;

		//</Free camera mode Variables>

		[SerializeField]
		private Transform lockedCameraPos;

		//<smoothing/damping variables>
		[SerializeField]
		private float camSmoothDampTime = 0.1f;
		[SerializeField]
		private float lookDirDampTime = 0.1f;
		private Vector3 velocityCamSmooth = Vector3.zero;
		private Vector3 velocityLookDir = Vector3.zero;
		//</smoothing/damping variables>

		private float TargetThershold = 0.01f;
		private float toggleCameraThreshold = 0.01f;
		private float toggleCameraTimer = 0.1f;

		private Vector3 lookDir;
		private Vector3 curLookDir;
		private Vector3 targetPosition;

		private CamStates camStates = CamStates.Free;
		private CameraPosition firstPersonCamPos;

		public GameObject pausePos;
		public GameObject pauseLookAt;
		//private CharacterControllerLogic characterController;
		Final_ThirdPersonController thirdPersonController;
		

	//Position camera at cave exit for cinematic

	int camIndex = 0;
	public int index = 0;


	//	private Vector3 lockedOffest;

		private bool isInFirstPerson = false;

		public CamStates CamState {
				get {
						return camStates;
				}
		set{camStates = value;}
		}


		public Transform ParentRig {
			get {
				return parentRig;
			}
			set {
				parentRig = value;
			}
		}

	//Different cam states 
		public enum CamStates
		{
			Behind,
			Locked,
			FirstPerson,
			Target,
			Free,
			Pause

		}


		void Start () {
		//	/// <summary>
			/// GetComponents.....
	
		index = 0;

	

			thirdPersonController = FindObjectOfType<Final_ThirdPersonController>();
			//followXForm = GameObject.FindGameObjectWithTag("Player").transform;
	

			/// <summary>
			/// Setting Variables.
			/// </summary>
			parentRig = this.transform.parent;
			lookDir = followXForm.forward;
			curLookDir = followXForm.forward;

			//lockedOffest = transform.position - followXForm.transform.position;
			//making a new instance of the struct and
			//Setting values from the struct
			firstPersonCamPos = new CameraPosition();
			firstPersonCamPos.Init
			(	
				 "First Person Camera",
				 new Vector3(-0.1f, 2f, 0.38f),
				 new GameObject().transform,
				 followXForm

			);

		}
	void Update()
	{
	

	}
		// Update is called once per frame
		void LateUpdate () {
		

			//Get values from keyboard / Controller	
			float rightX = Input.GetAxis("RightStickX");
			float rightY = Input.GetAxis("RightStickY");
			float leftX = Input.GetAxis("Horizontal");
			float leftY = - Input.GetAxis("Vertical");
			
			Vector3 characterOffset = followXForm.position + new Vector3(0f, distanceUp, distanceAway);
			Vector3 lookAt = characterOffset;
			Vector3 targetPosition = Vector3.zero;	
			//if(!inventory.showInventory)
		//	{
				//Targeting mode
				if (Input.GetAxis("Target") > TargetThershold)
				{
					camStates = CamStates.Target;
					//characterController.targeting = true;
				}
			

			 //First person
			// if the right stick is pressed, we aren't in free mode and we are not in locomotion
				if (Input.GetButtonDown("ToggleCameraMode") ) {
				// reset look before entering into first person mode
				ResetCamera();
				xAxisRot = 0;
				lookWeight = 0f;
				FirstPeron();
				
			}
					


				
		//	}


	//		characterController.Animator.SetLookAtWeight(lookWeight);
			//*********************Changing the Camera angle / logic *********************************
			//********************* To meet what we determined above**********************************

			switch (camStates) {
			case CamStates.Behind:
					ResetCamera ();
	
			// Only update camera look direction if moving

			lookDir = Vector3.Lerp (followXForm.right * (leftX < 0 ? 1f : -1f), followXForm.forward * (leftY < 0 ? -1f : 1f), 
                       Mathf.Abs (Vector3.Dot (this.transform.forward, followXForm.forward)));

	
				// Calculate direction from camera to player, kill Y, and normalize to give a valid direction with unit magnitude
				curLookDir = Vector3.Normalize (characterOffset - this.transform.position);
				curLookDir.y = 0;

	
			// Damping makes it so we don't update targetPosition while pivoting; camera shouldn't rotate around player
				curLookDir = Vector3.SmoothDamp (curLookDir, lookDir, ref velocityLookDir, lookDirDampTime);
			

				targetPosition = characterOffset + followXForm.up * distanceUp - Vector3.Normalize (curLookDir) * distanceAway;
	
				break;

			//Targeting code for camera
		case CamStates.Target:			
			ResetCamera ();
			lookDir = followXForm.forward;
			curLookDir = followXForm.forward;
		
			targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;

			break;


			//first person code for camera
		case CamStates.FirstPerson:	
	

			// Looking up and down
			// Calculate the amount of rotation and apply to the firstPersonCamPos GameObject
			xAxisRot += (rightY * firstPersonLookSpeed);			
			xAxisRot = Mathf.Clamp (xAxisRot, firstPersonXAxisClamp.x, firstPersonXAxisClamp.y - 22); 
			firstPersonCamPos.XForm.localRotation = Quaternion.Euler (xAxisRot, 0, 0);

			// Superimpose firstPersonCamPos GameObject's rotation on camera
			Quaternion rotationShift = Quaternion.FromToRotation (this.transform.forward, firstPersonCamPos.XForm.forward);		
			this.transform.rotation = rotationShift * this.transform.rotation;		


			//supposed to mover characters head when in fpv but it doesnt?
			//characterController.Animator.SetLookAtPosition(firstPersonCamPos.XForm.position + firstPersonCamPos.XForm.forward);
			lookWeight = Mathf.Lerp (lookWeight, 1.0f, Time.deltaTime * firstPersonLookSpeed);

			//moving camera LEFT/RIGHT when in fpv
			//pretty much same as in character controller when we rotate character
			Vector3 rotationAmount = Vector3.Lerp (Vector3.zero, new Vector3 (0f, fPSRotationDegreePerSecond * (rightX < 0f ? -1f : 1f), 0f), Mathf.Abs (rightX));
			Quaternion deltaRotation = Quaternion.Euler (rotationAmount * Time.deltaTime * 10);
			thirdPersonController.transform.rotation = (thirdPersonController.transform.rotation * deltaRotation );
			// Move camera to firstPersonCamPos
			targetPosition = firstPersonCamPos.XForm.position;


			// Smoothly transition look direction towards firstPersonCamPos when entering first person mode from sides
			lookAt = Vector3.Lerp (targetPosition + followXForm.forward, this.transform.position + this.transform.forward, camSmoothDampTime * Time.deltaTime);

			lookAt = (Vector3.Lerp (this.transform.position + this.transform.forward, lookAt, Vector3.Distance (this.transform.position, firstPersonCamPos.XForm.position)));
			//transform.LookAt(lookAt);
			break;

	case CamStates.Free:	
			lookWeight = Mathf.Lerp (lookWeight, 0.0f, Time.deltaTime * firstPersonLookSpeed);

			//move height and distance from character with parent rig...
			Vector3 rigToGoalDirection = Vector3.Normalize (characterOffset - this.transform.position);
			// zero out y direction 
			rigToGoalDirection.y = 0;

			Vector3 rigToGoal = characterOffset - parentRig.position;
			rigToGoal.y = 0;
			//Debug.DrawRay(parentRig.transform.position, rigToGoal, Color.red);
			/// <summary>
			/// moving Camera In And Out
			/// IF statement works for positive values, don't tween if stick not increasing in either direction
			/// Dont tween if user is rotating 
			/// check again rightX THRESHOLD, small values for RIGHTY will stuff uo the Lerp function
			/// </summary>

			if (rightY < -1f * rightStickThreshold && rightY <= rightStickPrevFrame.y )
			{
				dstUpFree = Mathf.Lerp(distanceUp, distanceUp * distanceUpMultiplyer, Mathf.Abs(rightY));
				dstAwayFree = Mathf.Lerp(distanceAway, distanceAway * distanceAwayMultiplyer, Mathf.Abs(rightY));
				targetPosition = characterOffset + followXForm.up * dstUpFree - rigToGoalDirection * dstAwayFree;


			}
			else if (rightY > rightStickThreshold && rightY >= rightStickPrevFrame.y )
			{
				// Subtract height of camera from height of player to find Y distance
				dstUpFree = Mathf.Lerp(Mathf.Abs(transform.position.y - characterOffset.y), camMinDistFromChar.y, rightY);
				// Use magnitude function to find X distance	
				dstAwayFree = Mathf.Lerp(rigToGoal.magnitude, camMinDistFromChar.x, rightY);
				
				targetPosition = characterOffset + followXForm.up * dstUpFree - rigToGoalDirection * dstAwayFree;
			}

				dstUpFree = 1.5f;
				dstAwayFree = 3.5f;
				targetPosition = characterOffset + followXForm.up * dstUpFree - rigToGoalDirection * dstAwayFree;
				if (rightX != 0 || rightY != 0) {
					saveRigToGoal = rigToGoalDirection;
				}
				
				parentRig.RotateAround (characterOffset, followXForm.up, freeRotationDegreePerSecond * (Mathf.Abs (rightX) > rightStickThreshold ? rightX : 0f) * Time.deltaTime);

				if (targetPosition == Vector3.zero) {
					targetPosition = characterOffset + followXForm.up * dstUpFree - saveRigToGoal * dstAwayFree;
				}

				SmoothPosition(transform.position, targetPosition);
				break;

			case CamStates.Locked:
					//targetPosition = Vector3.Lerp( transform.position, new Vector3(lockedCameraPos.position.x, lockedCameraPos.position.y + distanceUp, lockedCameraPos.position.z - distanceAway),10);
					lookDir = followXForm.forward;
					curLookDir = followXForm.forward;
	
					targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;
					//characterController.LocomotionThreshold = 0.3f;
					//this.transform.forward = followXForm.transform.forward;
					break;
			case CamStates.Pause:
			targetPosition = pausePos.transform.position;
			// Smoothly transition look direction towards firstPersonCamPos when entering first person mode from sides
	
			lookAt = Vector3.Lerp (targetPosition + followXForm.forward, this.transform.position + this.transform.forward, camSmoothDampTime * Time.deltaTime);
			lookAt = (Vector3.Lerp (this.transform.position + this.transform.forward, lookAt, Vector3.Distance (this.transform.position, pausePos.transform.position)));
			lookAt = pauseLookAt.transform.position;
			//transform.LookAt(lookAt);

				break;
			}

//			
		//transform.LookAt(lookAt);
		SmoothPosition (this.transform.position, targetPosition);
		CompensateForWalls (characterOffset, ref targetPosition);

//					
	


		}


		private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
		{
			//Smooth camera
			this.transform.position = Vector3.SmoothDamp(fromPos,toPos, ref velocityCamSmooth, camSmoothDampTime, 1000 * Time.deltaTime, Time.deltaTime);
		}

		private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
		{
		int layerMask = 1 << 14;

				// raycast to detect walls and if a wall is hit stay at that position and dont move past wall
			RaycastHit wallHit = new RaycastHit();
		if (Physics.Linecast(fromObject, toTarget, out wallHit, layerMask)) {
				toTarget = Vector3.Lerp(transform.position, new Vector3 ( wallHit.point.x , toTarget.y, wallHit.point.z), 100 );
					}	
		}

		private void ResetCamera()
		{
			lookWeight = Mathf.Lerp(lookWeight, 0.0f, Time.deltaTime * firstPersonLookSpeed);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime);
		}


		private int ClampAngle (float angle, float min, float max) {
			if (angle < min) {
				angle = min;
				
			}
			else if (angle > max) {
				angle = max;
			}
			return Mathf.Clamp ((int)(angle), (int)(min), (int)(max));
		}

		private void FirstPeron()
		{

				if (isInFirstPerson == true) 
				{
					isInFirstPerson = false;
					camStates = CamStates.Free;
				}
				
				else if(isInFirstPerson == false)
				{
					isInFirstPerson = true;	
					camStates = CamStates.FirstPerson;
				}
				
			
		}

	}
