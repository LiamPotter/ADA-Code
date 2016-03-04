using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuControllerShip : MonoBehaviour {
	//This script needs to be placed on the camera in the scene
	public bool atMenu;
	public GameMechanics gameMech;
	public FlightGameManager flightGM;
	public bool lockedToMenu;
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
		if(gameMech==null)
			gameMech = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
		if (flightGM == null)
			flightGM = GameObject.FindGameObjectWithTag ("GameController").GetComponent<FlightGameManager> ();
		if(Input.GetButton("Pause")&&!atMenu&&canSwitch&&!flightGM.countDownActive)
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
			menuCanvas.SetActive(true);
			gameMech.mainCam.GetComponent<CustomPointer>().enabled=false;
			gameMech.mainCam.GetComponent<CameraFlightFollow>().enabled=false;
			gameMech.ship.GetComponent<ShipController>().enabled=false;
			gameMech.ship.GetComponent<Rigidbody>().isKinematic=true;
			if(eSystem.currentSelectedGameObject!=null)
				pointer.transform.localPosition = new Vector3 (eSystem.currentSelectedGameObject.transform.localPosition.x-75f,eSystem.currentSelectedGameObject.transform.localPosition.y-45f,0);
		}
		if(!atMenu)
		{
			menuCanvas.SetActive(false);
			gameMech.mainCam.GetComponent<CustomPointer>().enabled=true;
			gameMech.mainCam.GetComponent<CameraFlightFollow>().enabled=true;
			gameMech.ship.GetComponent<ShipController>().enabled=true;
			gameMech.ship.GetComponent<Rigidbody>().isKinematic=false;
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
