using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlightGameManager : MonoBehaviour {
	public bool flightGameActive;
	public bool countDownActive;
	public int countDownNumber;
	public Text countDownUI;

	public float startingFuelTime;
	private float currentFuelTime;
	public float addtionalFuelTimeAmount;

	public bool boosting;
	public bool debugBoost;
	public float boostSpeed;
	public float boostTime;
	public float boostWait;
	private Text boostText;
	public float currentBoostTime;

	public GameObject[] flightGames;
	public int numberOfFlightGames;
	public int flightGameSelected; //0 refers to FlightGame0, 1 refers to FlightGame1 and so forth

	public GameObject[] beacons;
	public int selectedBeacon;
	public int beaconAfter;
	public GameObject nextBeacon;
	public GameObject farBeacon;
	public GameObject lastBeaconToHit;
	public Material[] beaconMats;
	private bool lastBeacon;

	private GameMechanics gm;
	private ShipController shipC;
	public GameObject flightPointer;
	private Color defaultPColor;
	private Text timerText;
	public bool trackSetBool=false;


	void Start () 
	{
		gm = GetComponent<GameMechanics> ();
		shipC = GameObject.FindGameObjectWithTag ("Ship").GetComponent<ShipController> ();
		numberOfFlightGames = GameObject.FindGameObjectsWithTag ("FlightGame").Length;
		flightGames = new GameObject[numberOfFlightGames];
		timerText = GameObject.Find ("FlightGameTimerUI").GetComponent<Text>();
		boostText = GameObject.Find ("BoostText").GetComponent<Text>();boostText.enabled = false;
		countDownActive = flightGameActive;
		if (countDownActive)
			StartCoroutine ("RaceCountdown", 0.5f);
	}
	void Update () 
	{
		for(int i=0; i<flightGames.Length; i++)
		{
			if(flightGames[i]==null)
				flightGames[i] = GameObject.Find("FlightGame"+i);
		}
		if(countDownActive)
		{
			if(countDownNumber!=0)
			{
				shipC.enabled=false;
				shipC.gameObject.GetComponent<PlayerFlightControl>().enabled=false;
				gm.mainCam.GetComponent<CameraFlightFollow>().enabled=false;
				countDownUI.text=countDownNumber.ToString();
			}
			else if(countDownNumber==0)
			{
				shipC.gameObject.GetComponent<PlayerFlightControl>().enabled=true;
				gm.mainCam.GetComponent<CameraFlightFollow>().enabled=true;
				countDownUI.text="GO";
			}

		}
		if (!countDownActive)
		{
			countDownUI.enabled=false;
			StopCoroutine ("RaceCountdown");
		}
		if(!trackSetBool)
		{
			TrackSet (flightGames,flightGameSelected);
		}
		if (!flightGameActive)
		{
			DisableGame();
		}
		if(debugBoost)
		{
			shipC.GetComponent<PlayerFlightControl> ().afterburner_speed=boostSpeed*2;
		}
		if(trackSetBool&&flightGameActive&&countDownNumber==0)
			UpdatingGame ();
		if (currentFuelTime <= 0&&flightGameActive)
			LostFlightGame ();
//		if (flightGameSelected <= flightGames.Length)
//		{
//			print ("settingTrack");
//			TrackSet (flightGames,flightGameSelected);
//		}
	}
	public void TrackSet(GameObject[] fArray,int sFG)
	{

		for(int i=0; i<fArray.Length; i++)
		{
			if(fArray[i]==fArray[sFG])
			{
				fArray[i].SetActive(true);
			}
			if(fArray[i]!=fArray[sFG])
				fArray[i].SetActive(false);
		}
		int numberOfBeacons;
		numberOfBeacons = fArray [sFG].gameObject.transform.childCount;
		beacons = new GameObject[numberOfBeacons];
		for(int i=0; i<beacons.Length; i++)
		{
			if(beacons[i]==null)
			{
				beacons[i] = GameObject.Find("Beacon "+i);
			}
		}
		currentFuelTime=startingFuelTime;
		selectedBeacon = 0;
		beaconAfter = 1;
		nextBeacon = beacons [selectedBeacon];
		farBeacon = beacons [beaconAfter];
		lastBeaconToHit = beacons[beacons.Length-1];
		flightPointer.SetActive (true);

		trackSetBool = true;
//		nextBeacon.SetActive (true);
//		farBeacon.SetActive (true);	
	}
	public void UpdatingGame()
	{
		Vector3 relativePos = nextBeacon.transform.position- flightPointer.transform.position;
		Quaternion rotation = Quaternion.LookRotation(relativePos,transform.up);
		flightPointer.transform.rotation = Quaternion.Slerp(flightPointer.transform.rotation, rotation, 5 * Time.deltaTime);
		//flightPointer.transform.rotation = rotation;
		//flightPointer.transform.LookAt(nextBeacon.transform);
		if (shipC.enabled = true)
			shipC.enabled = false;
		if(timerText.enabled==false)
			timerText.enabled = true;
		if (boosting)
			Boosting ();
		if (!boosting)
		{
			shipC.GetComponent<PlayerFlightControl> ().afterburner_speed = 150f;
			for(int i=0; i<shipC.defaultParticleSystems.Length; i++)
			{
				shipC.defaultParticleSystems[i].GetComponent<ParticleSystemRenderer>().enabled=true;
			}
			for(int i=0; i<shipC.boostParticleSystems.Length; i++)
			{
				shipC.boostParticleSystems[i].GetComponent<ParticleSystemRenderer>().enabled=false;
			}
		}
		if (flightGameActive&&currentFuelTime>0f&&!gm.shipMenuController.atMenu) 
		{
			currentFuelTime-=Time.deltaTime;
			timerText.text="Fuel Time: "+currentFuelTime.ToString("F2")+"s";
		}
		if (currentFuelTime < 0f)
		{
			currentFuelTime = 0;
			timerText.text="Fuel Time: "+currentFuelTime.ToString("F2")+"s";
		}

		for(int i = 0; i < beacons.Length; i++)
		{
			if(flightGameActive)
			{
				if(nextBeacon!=beacons[i])
				{		
					beacons[i].SetActive(false);
				}
				if(farBeacon!=beacons[i])
				{
					beacons[i].SetActive(false);
				}
				if(nextBeacon==beacons[i])
				{
					beacons[i].SetActive(true);
					if(beacons[i].tag!="BeaconBooster")
						beacons[i].GetComponent<MeshRenderer>().material=beaconMats[0];
					else if(beacons[i].tag=="BeaconBooster")
						beacons[i].GetComponent<MeshRenderer>().material=beaconMats[2];
				}
				if(farBeacon==beacons[i]&&!lastBeacon)
				{
					beacons[i].SetActive(true);
					if(beacons[i].tag!="BeaconBooster")
						beacons[i].GetComponent<MeshRenderer>().material=beaconMats[1];
					else if(beacons[i].tag=="BeaconBooster")
						beacons[i].GetComponent<MeshRenderer>().material=beaconMats[3];
				}
			}
			if(!flightGameActive)
				beacons[i].SetActive(false);
		}
		if (flightGameActive) {
			flightPointer.SetActive (true);
		} else {
			flightPointer.SetActive(false);	
		}
	}
	public void BeaconHit(Collider hit)
	{
		hit.gameObject.SetActive (false);
		print (hit.name);
		if(selectedBeacon<=beacons.Length-1)
		{
			selectedBeacon++;
			//nextBeacon = beacons [selectedBeacon];
		}
		if(hit.tag=="BeaconBooster")
		{
			boosting=true;
			currentBoostTime=boostTime;
			currentFuelTime += addtionalFuelTimeAmount/2;
		}
		if(hit.tag!="BeaconBooster")
			currentFuelTime += addtionalFuelTimeAmount;
		if (beaconAfter >= beacons.Length)
		{
			beaconAfter = beacons.Length;
		}
		if(selectedBeacon<=beacons.Length-1)
		{
			nextBeacon = beacons [selectedBeacon];
		}
		if(selectedBeacon<=beacons.Length-2)
		{
			beaconAfter = selectedBeacon + 1;
			farBeacon = beacons[beaconAfter];
		}
		if(selectedBeacon==beacons.Length-1)
		{
			lastBeacon=true;
		}
		if(hit.name==lastBeaconToHit.name)
		{
			print ("deletingLast");
			nextBeacon.SetActive(false);
			flightGameActive=false;
		}
	}
	public void Boosting ()
	{
		if (currentBoostTime>0f) 
		{
			for(int i=0; i<shipC.defaultParticleSystems.Length; i++)
			{
				shipC.defaultParticleSystems[i].GetComponent<ParticleSystemRenderer>().enabled=false;
			}
			for(int i=0; i<shipC.boostParticleSystems.Length; i++)
			{
				shipC.boostParticleSystems[i].GetComponent<ParticleSystemRenderer>().enabled=true;
			}
			boostText.enabled=true;
			currentBoostTime-=Time.deltaTime;
		}
		if (currentBoostTime <= 0f)
		{
			boostText.enabled=false;
			boosting = false;
		}
		if(currentBoostTime>boostWait)
			shipC.GetComponent<PlayerFlightControl> ().afterburner_speed=boostSpeed;
		else if(currentBoostTime<boostWait)
		{
			if(shipC.GetComponent<PlayerFlightControl> ().afterburner_speed>100f)
			{
				shipC.GetComponent<PlayerFlightControl> ().afterburner_speed =Mathf.Lerp(shipC.GetComponent<PlayerFlightControl> ().afterburner_speed,100f,(Time.deltaTime*0.5f));
			}
		}
		if (shipC.GetComponent<PlayerFlightControl> ().afterburner_speed <= 100)
			shipC.GetComponent<PlayerFlightControl> ().afterburner_speed = 100;
	}
	public void DisableGame ()
	{
		currentFuelTime=startingFuelTime;
		flightPointer.SetActive(false);
		timerText.text="Fuel Time: ";
		boostText.enabled=false;
		timerText.enabled = false;
		boosting = false;
		for(int i = 0; i < beacons.Length; i++)
		{
			beacons[i].SetActive(false);
		}
		trackSetBool = true;
	}
	public void LostFlightGame()
	{
		print ("You Lost");
		shipC.GetComponent<PlayerFlightControl> ().enabled = false;
		shipC.GetComponent<Rigidbody> ().velocity=Vector3.Lerp(shipC.GetComponent<Rigidbody> ().velocity,Vector3.zero, 0.8f*Time.deltaTime);
//		shipC.particlesDefault.GetComponent<ParticleSystem>().Stop();shipC.particlesDefault.GetComponentInChildren<ParticleSystem>().Stop ();
		flightGameActive = false;
	}
	IEnumerator RaceCountdown()
	{
		countDownNumber = 3;
		yield return new WaitForSeconds (1f);
		countDownNumber = 2;
		yield return new WaitForSeconds (1f);
		countDownNumber = 1;
		yield return new WaitForSeconds (1f);
		countDownNumber = 0;
		yield return new WaitForSeconds (1f);
		countDownActive = false;
	}
}
