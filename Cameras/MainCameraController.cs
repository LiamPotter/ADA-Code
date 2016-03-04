using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	public GameObject player;
	public GameObject ship;
	public GameMechanics gm;
	public bool camSwitch=false;
	public bool check = false;
	private bool c;

	public float journeySpeed=1.0f;
	//where the player's default camera should be
	public Transform playerCamPos;
	//where the ship's default camera should be
	public Transform shipCamPos;
	float startTime;
	private NewCameraController nCC;
	public PlayerFlightControl pFC;
	public CameraFlightFollow cFF;
	public bool tweenEnd=true;
	public bool pCam;
	void Start () 
	{
		gm=GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
	}
	void Awake()
	{
		cFF= GetComponent<CameraFlightFollow>();
		nCC = GetComponent<NewCameraController> ();
		//tw = GetComponent<Tweening> ();
	}
	public void ParentSwitch()
	{
		camSwitch = !camSwitch;
		check = true;
		c = true;
	}
	void Update () 
	{
	
		float currentDuration = (Time.time)*journeySpeed - startTime;
		if (camSwitch&&check)
		{
			print ("stuck1");
			tweenEnd=false;
			//this.gameObject.transform.SetParent (ship.gameObject.transform);
			this.gameObject.GetComponent<CustomPointer>().enabled=true;
		//	this.gameObject.GetComponent<NewCameraController>().enabled=false;
			player.SetActive(false);
			nCC.enabled=false;
			if(!gm.insideShip&&gm.selectedPlanet!=null)
				gm.selectedUI.SetActive(true);
			check=false;
		}
		if (!camSwitch&&check&&gm.spawnPoint!=null)
		{
			print ("stuck2");
			tweenEnd = false;
			//this.gameObject.transform.SetParent (player.gameObject.transform);
			this.gameObject.GetComponent<CustomPointer>().enabled=false;
			this.gameObject.GetComponent<CameraFlightFollow>().enabled=false;
			pFC.enabled=false;
			//this.gameObject.GetComponent<NewCameraController>().enabled=true;
			nCC.enabled = true;
			if(!gm.insideShip)
				gm.selectedUI.SetActive(false);
			check=false;
		}
		if(camSwitch&&player!=null)
		{
			if(c)
			{
				transform.position=shipCamPos.position;
				cFF.enabled=true;
				pFC.enabled=true;
				c=false;
			}

		}
		if(!camSwitch&&player!=null)
		{
			if(!check)
			{
				transform.position=playerCamPos.position;
				check =true;
			}

			transform.position=playerCamPos.position;
			transform.rotation=playerCamPos.rotation;

		
		}
	


	}
}
