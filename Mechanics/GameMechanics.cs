using UnityEngine;
using System.Collections;

public class GameMechanics : MonoBehaviour {

	//the player
	public GameObject player;
	//player's camera rig
	public GameObject cameraRig;
	//the spaceship
	public GameObject ship;
	//the ship's camera
	public GameObject mainCam;
	//UI for each planet
	public GameObject selectedUI;
	public bool selectUiAllowed;
	//the bool that controls switching between ship and player cameras
	public bool playerCam = false;
	//gets set whenever the ship enters the trigger around each planet
	public GameObject selectedPlanet;
	//the interior object of the ship
	public GameObject shipInterior;
	//each planet needs a spawnpoint named spawnpoint
	public Transform spawnPoint;

	public MenuControllerShip shipMenuController;

	public bool insideShip;
	//stops the spawner after one spawn
	public bool spawnCheck;
	public bool flying;
	private bool check;
	private bool doubleCheck;
	void Start () 
	{
		shipMenuController = GameObject.Find ("MainCameraShip").GetComponent<MenuControllerShip> ();
	}

	void Update () 
	{

		if(selectedPlanet!= null&&!insideShip)
		{	
			spawnPoint = selectedPlanet.transform.FindChild ("Spawnpoint");
			selectedUI = selectedPlanet.transform.FindChild("PlanetCanvas").gameObject;
		}
		if(Input.GetButtonDown("Submit")&&spawnPoint!=null&&insideShip)
		{
			Beaming();
		}
		if(playerCam)
		{

			if(!insideShip)
				selectedPlanet.transform.parent.GetComponent<MeshRenderer>().enabled=true;
			if(spawnCheck)
			{
				if(!insideShip)
					cameraRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam>().isOnPlanet=true;
				print ("spawned");
				flying=false;
				mainCam.SetActive(false);
				player.SetActive(true);
				cameraRig.SetActive(true);
				player.transform.position = spawnPoint.transform.position;
				player.transform.GetChild(0).position= spawnPoint.transform.position;
				cameraRig.transform.position=spawnPoint.transform.position;
				player.transform.SetParent(selectedPlanet.transform);cameraRig.transform.SetParent(selectedPlanet.transform);
				//player.gameObject.GetComponent<Rigidbody>().velocity=Vector3.zero;
				spawnCheck=false;
			}
		
			//ship.transform.SetParent(selectedPlanet.transform);

			//mainCam.gameObject.SetActive(false);
		}
		if(flying)
		{
			//print("flying");
			mainCam.SetActive(true);
			ship.GetComponent<PlayerFlightControl>().enabled=true;
			ship.GetComponent<Rigidbody>().isKinematic=false;
			//ship.transform.SetParent(shipInterior.transform);
		}
		if (!flying) 
		{
			//print("notflying");
			mainCam.SetActive(false);
			ship.GetComponent<PlayerFlightControl>().enabled=false;
			ship.GetComponent<Rigidbody>().isKinematic=true;
			mainCam.GetComponent<CustomPointer>().enabled=false;
			mainCam.GetComponent<CameraFlightFollow>().enabled=false;
			mainCam.GetComponent<Camera>().enabled=false;
			mainCam.GetComponent<AudioListener>().enabled=false;
		}
		if(!playerCam)
		{
			if(selectedPlanet!= null&&!insideShip)
				selectedPlanet.transform.parent.GetComponent<MeshRenderer>().enabled=false;
			//selectedPlanet.transform.GetComponentInParent<MeshRenderer>().enabled=false;
			if(check)
			{
				//player.SetActive(false);
				//cameraRig.SetActive(false);
				player.transform.SetParent(ship.GetComponent<ShipController>().intSpawn.transform);cameraRig.transform.SetParent(ship.GetComponent<ShipController>().intSpawn.transform);
				ship.GetComponent<ShipController>().insideShip=true;
				player.transform.rotation = Quaternion.identity;
				player.transform.GetChild(0).transform.rotation = Quaternion.identity;
				check=false;
			}
		
		}

	}

	public void Beaming()
	{
		spawnCheck = true;
		check=true;
		insideShip = false;
		playerCam = true;
	}
}
