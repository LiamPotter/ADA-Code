using UnityEngine;
using System.Collections;

public class NewCameraController : MonoBehaviour {
	public Transform camPos;
	public Transform targetPos;
	public Transform firstPersonCamPos;	
	public GameObject player;
	public bool firstPerson;
	public bool targeting;
	bool normalMode = true;
	public float distanceUp = 1;
	public float distanceAway = 3;
	private Vector3 lookDir;
	private Vector3 curLookDir;
	private Vector3 targetPosition;
	[SerializeField]
	private float camSmoothDampTime = 0.1f;
	[SerializeField]
	private float lookDirDampTime = 0.1f;
	private Vector3 velocityCamSmooth = Vector3.zero;
	private Vector3 velocityLookDir = Vector3.zero;
	GameMechanics gm;
	MainCameraController mCC;
	// Use this for initialization
	void Start () {
		gm = FindObjectOfType<GameMechanics> ();
		mCC = this.gameObject.GetComponent<MainCameraController> ();
	}
	
	// Update is called once per frame
	void Update () { 
		//player = GameObject.FindGameObjectWithTag("Player");
		if (gm.playerCam&&player!=null&&mCC.pCam) {

			player = GameObject.FindGameObjectWithTag("Player");
		
			//transform.rotation =  Quaternion.Euler(transform.rotation.x, transform.rotation.y, player.transform.rotation.z);
			float rightX = Input.GetAxis ("RightStickX");
			float rightY = Input.GetAxis ("RightStickY");
			float leftX = Input.GetAxis ("Horizontal");
			float leftY = Input.GetAxis ("Vertical");
	
			Vector3 characterOffset = player.transform.position + new Vector3 (0f, distanceUp, 0f);
		
			Vector3 lookAt = characterOffset;
			Vector3 targetPosition = Vector3.zero;	

			if (Input.GetButtonDown ("ToggleCameraMode")) {

				if (firstPerson == true) {
					firstPerson = false;
					normalMode = true;

				} else {
					firstPerson = true;
					normalMode = false;
				}
			} else if (Input.GetAxis ("Target") > 0.1 && !firstPerson) {

				lookDir = targetPos.transform.forward;
				curLookDir = targetPos.transform.forward;

				targetPosition = targetPos.position;
				SmoothPosition (transform.position, targetPosition);

				this.transform.localEulerAngles = new Vector3 (22.735f, transform.localEulerAngles.y, transform.localEulerAngles.z);

				targeting = true;
			} else {
				targeting = false;
				normalMode = true;
			}

			if (!firstPerson && !targeting && normalMode) {
				transform.position = camPos.position;
				transform.LookAt (player.transform);

			} else if (firstPerson && !targeting) {
				firstPerson = true;
				SmoothPosition(transform.position, firstPersonCamPos.position);
			//	transform.LookAt (player.transform);
				//transform.rotation = firstPersonCamPos.rotation;
			}
		}

	}
	private void SmoothPosition(Vector3 fromPos, Vector3 toPos)
	{
		//Smooth camera
		this.transform.position = Vector3.SmoothDamp(fromPos,toPos, ref velocityCamSmooth, camSmoothDampTime, 10000 * Time.deltaTime, Time.deltaTime);
	}

}
