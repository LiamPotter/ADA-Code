using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuControllerPlanet : MonoBehaviour {
	//This script needs to be placed on the camera in the scene
	public bool atMenu;
	//public GameMechanics gameMech;
	public bool lockedToMenu;
	public GameObject cameraRig;
	public GameObject ada;
	public GameObject menuCanvas;
	public GameObject pointer;
	public EventSystem eSystem;
	public bool canSwitch=true;
	void Awake()
	{
		canSwitch = true;
	}
	void Update () 
	{
		//if(gameMech==null)
			//gameMech = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();

		if(Input.GetButton("Pause")&&!atMenu&&canSwitch)
		{
			atMenu=true;
			canSwitch=false;
		}
		if (Input.GetButtonUp ("Pause") && !canSwitch)
			canSwitch = true;
		if(Input.GetButton("Pause")&&atMenu&&!lockedToMenu&&canSwitch)
		{
			atMenu=false;
			canSwitch=false;
		}
		if (atMenu)
		{
			if(menuCanvas.activeSelf==false)
				menuCanvas.SetActive(true);
			if(cameraRig.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip> ().enabled ==true)
				cameraRig.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip> ().enabled = false;
			if(cameraRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam> ().enabled ==true)
				cameraRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam> ().enabled = false;
			if(ada.GetComponent<PhysicsCharacterMotor>().enabled==true)
				ada.GetComponent<PhysicsCharacterMotor>().enabled=false;
			if(ada.GetComponent<PlatformCharacterController>().enabled==true)
				ada.GetComponent<PlatformCharacterController>().enabled=false;
			if(ada.GetComponent<Rigidbody>().isKinematic==false)
				ada.GetComponent<Rigidbody>().isKinematic=true;
			if(eSystem.currentSelectedGameObject!=null)
				pointer.transform.localPosition = new Vector3 (eSystem.currentSelectedGameObject.transform.localPosition.x-75f,eSystem.currentSelectedGameObject.transform.localPosition.y-45f,0);
		}
		if(!atMenu)
		{
			if(menuCanvas.activeSelf==true)
				menuCanvas.SetActive(false);
			if(cameraRig.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip> ().enabled ==false)
				cameraRig.GetComponent<UnityStandardAssets.Cameras.ProtectCameraFromWallClip> ().enabled = true;
			if(cameraRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam> ().enabled ==false)
				cameraRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam> ().enabled = true;
			if(ada.GetComponent<PhysicsCharacterMotor>().enabled==false)
				ada.GetComponent<PhysicsCharacterMotor>().enabled=true;
			if(ada.GetComponent<PlatformCharacterController>().enabled==false)
				ada.GetComponent<PlatformCharacterController>().enabled=true;
			if(ada.GetComponent<Rigidbody>().isKinematic==true)
				ada.GetComponent<Rigidbody>().isKinematic=false;
		}
	}

	public void PressedResume()
	{
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
		Application.LoadLevel("MainMenu");
	}
}
