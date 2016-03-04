using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {
	//This script needs to be placed on the camera in the scene
	public bool atMenu;
	public bool lockedToMenu;
	public GameObject menuCanvas;
	public GameObject pointer;
	public Quaternion mainCamRot;
	public float movementSmoothing = 7f;
	public GameObject cameraRig;
	public GameObject ada;
	public Animator adaAnimator;
	public Transform defaultCamPos;
	public Transform menuPosition;
	public Transform partWayPosition;
	public Transform menuLookAt;
	public bool finishedMovement=true;
	public EventSystem eSystem;

	void Update () 
	{
		if(Input.GetButtonUp("Pause")&&!atMenu&&finishedMovement)
		{
			StartCoroutine(MoveCoroutine(partWayPosition,menuPosition));	
			atMenu=true;
		}
		if(Input.GetButton("Pause")&&atMenu&&finishedMovement&&!lockedToMenu)
		{
			StartCoroutine(MoveCoroutine(partWayPosition,defaultCamPos));	
			atMenu=false;
		}
		if (atMenu)
		{
			menuCanvas.SetActive(true);
			if(eSystem.currentSelectedGameObject!=null)
				pointer.transform.localPosition = new Vector3 (eSystem.currentSelectedGameObject.transform.localPosition.x-30f,eSystem.currentSelectedGameObject.transform.localPosition.y-25f,0);
			cameraRig.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip> ().enabled = false;
			cameraRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam> ().enabled = false;
			ada.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled=false;
			ada.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled=false;
			Vector3 relativePos = menuLookAt.position- transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
			adaAnimator.speed=0;
		}
		if(!atMenu)
		{
			menuCanvas.SetActive(false);
		}
		if(!atMenu&&finishedMovement)
		{
			cameraRig.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip> ().enabled = true;
			cameraRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam> ().enabled = true;
			ada.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter>().enabled=true;
			ada.GetComponent<UnityStandardAssets.Characters.ThirdPerson.ThirdPersonUserControl>().enabled=true;
			adaAnimator.speed=1;
			transform.rotation=new Quaternion(0,0,0,0);
		}

		if(!atMenu&&!finishedMovement)
		{
			print ("derp");
			Vector3 relativePos = menuLookAt.position- transform.position;
			Quaternion rotation = Quaternion.LookRotation(relativePos);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation,50*Time.deltaTime);
		}

	}
	IEnumerator MoveCoroutine(Transform target1,Transform target2)
	{
		while (Vector3.Distance(transform.position,target1.position)>0.15f)
		{
			print("movingToTarget1");
			transform.position=Vector3.Lerp(transform.position, target1.position,movementSmoothing*Time.deltaTime);
			finishedMovement=false;
			yield return null;
		}
		while (Vector3.Distance(transform.position,target2.position)>0.1f)
		{
			print("movingToTarget2");
			finishedMovement=false;
			transform.position=Vector3.Lerp(transform.position, target2.position,movementSmoothing/2*Time.deltaTime);
			yield return null;
		}
		finishedMovement = true;
	}
	public void PressedResume()
	{
		StartCoroutine(MoveCoroutine(partWayPosition,defaultCamPos));	
		atMenu=false;
	}
	public void PressedSpaceRace()
	{
		Application.LoadLevel("FlightGame");
	}
	public void PressedPlanetExploration()
	{
		Application.LoadLevel("PlanetScene");
	}
	public void PressedDungeonCrawl()
	{
		Application.LoadLevel("Exhibition Dungeon");
	}
	public void PressedQuitGame()
	{
		Application.Quit ();
	}
}
