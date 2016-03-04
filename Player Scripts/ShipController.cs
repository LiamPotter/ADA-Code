using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public GameMechanics gm;
	public MapCameraScript mapCC;
	public bool insideShip;
	public MeshCollider exterior;
	public Collider interior;
	public Transform intSpawn;
	public GameObject gravityWell;
	public ParticleSystem[] defaultParticleSystems;
	public ParticleSystem[] boostParticleSystems;
	public Color currentParticleColor;
	//public PlayerFlightControl PlayerFC;
	//public float vForceStrength = 4000f;
	public float vForce = 10f;
	private bool checker=true;
	public FlightGameManager fGM;
	public GameObject playerCamRig;
	void Start()
	{
		gm =GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
		fGM = GameObject.FindGameObjectWithTag ("GameController").GetComponent<FlightGameManager> ();
	}
	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "Activator")
		{
			gm.selectedPlanet = hit.transform.parent.gameObject;
			this.transform.SetParent(hit.transform.parent.gameObject.transform);
			gm.selectedUI.GetComponent<PlanetUI>().textOn=false;
			print ("planetUIFalse");
		}
		if(hit.gameObject.tag == "Beacon"||hit.gameObject.tag=="BeaconBooster")
			fGM.BeaconHit(hit);

	}
	void OnTriggerStay(Collider hit)
	{
		if(hit.gameObject.tag == "Activator")
		{
			gm.selectedUI.GetComponent<PlanetUI>().textOn=false;
		}
	}
	void OnTriggerExit(Collider hit)
	{
		if(hit.gameObject.tag=="Activator")
		{
			gm.selectedPlanet= null;
			gm.spawnPoint = null;
			this.transform.SetParent(null);
			gm.selectedUI.GetComponent<PlanetUI>().textOn=true;
		}
	}
	void Update()
	{
		Vector3 forward = transform.TransformDirection(Vector3.forward) * 250;
		Debug.DrawRay(transform.position, forward, Color.green);
		InShip ();
		OutShip();
		if(Input.GetButtonDown("Submit")&&!insideShip&&!gm.playerCam)
		{
			insideShip=true;
			checker=false;
		}
		if(Input.GetButtonDown("B Button")&&insideShip&&!mapCC.toMap&&!mapCC.hasPickedWorld)
		{
			print ("exitingShip");
			insideShip=false;
			checker=false;
		}
	}
	public void InShip()
	{
		if(!checker&&insideShip)
		{
			print("inShip");
			//gm.selectedPlanet=interior.gameObject;
//			gm.player.GetComponentInChildren<GravityBody>().planet=gravityWell.GetComponent<GravityAttractor>();
			gm.flying=false;
			GetComponent<Rigidbody>().constraints=RigidbodyConstraints.FreezePosition|RigidbodyConstraints.FreezeRotation;
			gm.insideShip=true;
			gm.ship.GetComponent<PlayerFlightControl>().enabled=false;
			for(int i=0; i<defaultParticleSystems.Length; i++)
			{
				defaultParticleSystems[i].gameObject.SetActive(false);
			}

			exterior.enabled=false;
			gm.player.transform.position=intSpawn.position;
			gm.player.transform.GetChild(0).transform.position=intSpawn.position;
	
			gm.cameraRig.SetActive(true);
			gm.player.SetActive(true);
			gm.playerCam=true;
			gravityWell.transform.parent=gm.player.transform.GetChild(0).transform;
			//gravityWell.transform.localPosition=gm.player.transform.localPosition;
			gm.mainCam.GetComponent<MainCameraController>().ParentSwitch();
			playerCamRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam>().isOnPlanet = false;
			checker=true;
		}
	}
	public void OutShip()
	{
		if(!checker&&!insideShip)
		{
			print("outShip");
			gm.selectedPlanet=gm.selectedPlanet;
			gm.playerCam=false;
			GetComponent<Rigidbody>().constraints=RigidbodyConstraints.None;
			gm.insideShip=false;
			gm.ship.GetComponent<PlayerFlightControl>().enabled=true;
			for(int i=0; i<defaultParticleSystems.Length; i++)
			{
				defaultParticleSystems[i].gameObject.SetActive(true);
			}
			gm.mainCam.GetComponent<CustomPointer>().enabled=true;
			gm.mainCam.GetComponent<CameraFlightFollow>().enabled=true;
			exterior.enabled=true;
			gm.mainCam.SetActive(true);	
			gm.mainCam.GetComponent<Camera>().enabled=true;
			gm.mainCam.GetComponent<AudioListener>().enabled=true;
			gm.cameraRig.SetActive(false);
			gm.player.SetActive(false);
			gravityWell.transform.parent=intSpawn;
			gravityWell.transform.localPosition=intSpawn.localPosition;gravityWell.transform.localPosition=new Vector3(0,-5,0);
			gm.mainCam.GetComponent<MainCameraController>().ParentSwitch();
			playerCamRig.GetComponent<UnityStandardAssets.Cameras.FreeLookCam>().isOnPlanet = true;
			checker=true;
		}
	}
}
