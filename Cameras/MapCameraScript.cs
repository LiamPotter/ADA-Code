using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapCameraScript : MonoBehaviour {

	public GameMechanics gameM;
	private MainCameraController mainCC;
	private ShipController shipC;
	public GameObject ship;
	public GameObject midPanel;
	public GameObject playerCam;
	public bool toMap = false;
	public Transform mapCamPos;
	public Transform playerCamPos;
	public GameObject[] mapSegments;
	public Transform[] mapNulls;
	public RotateAround[] mapCams;
	public GameObject[] blueSpawnPoints;
	public GameObject selectedItem;
	private CameraFlightFollow camFF;
	private CustomPointer customP;
	private bool checkLock1=true;
	private bool checkLock2;
	private bool checkLock3=false;
	private bool checkLock4=false;
	public bool hasPickedWorld;
	//public Animator selectedAnimator;
	public bool check;

	private Transform savedPos;
	public float mapMovementSpeed;
	private float canvasAlpha;
	private float startTime;
	private float journeyLength;
	void Start()
	{
		mainCC = GetComponent<MainCameraController> ();
		camFF = GetComponent<CameraFlightFollow> ();
		customP = GetComponent<CustomPointer> ();
		shipC = ship.GetComponent<ShipController> ();
		selectedItem = mapSegments [0];
		hasPickedWorld = false;
		startTime = Time.time;
	}
	void Update () 
	{
		if(toMap)
		{
			if(checkLock1)
			{
				for(int i = 0; i < mapSegments.Length; i++)
				{
					mapSegments[i].gameObject.SetActive(true);
				}
				gameM.player.SetActive(false);
				playerCam.SetActive(false);
				gameM.mainCam.GetComponent<Camera>().enabled=true;
				gameM.mainCam.GetComponent<AudioListener>().enabled=true;
				mainCC.enabled=false;
				EventSystem.current.SetSelectedGameObject(mapSegments[0]);
				camFF.enabled=false;
				customP.enabled=false;
				transform.position = mapCamPos.position;
				transform.rotation = mapCamPos.rotation;
				transform.parent = mapCamPos;
				ship.GetComponent<ShipController>().enabled=false;
				ship.GetComponent<Rigidbody>().isKinematic = true;
				ship.GetComponent<Rigidbody>().velocity=Vector3.zero;
				selectedItem.gameObject.GetComponent<Animator>().SetBool("selected",true);
				checkLock1=false;
			}
			if(selectedItem==null)
			{
				EventSystem.current.SetSelectedGameObject(mapSegments[0]);
			}
			selectedItem=EventSystem.current.currentSelectedGameObject;
		
			if(Input.GetButtonDown("B Button")&&!hasPickedWorld)
			{
				print ("backing out of map");
				toMap=false;
				checkLock2=true;
			}
			if(Input.GetButtonDown("A Button")&&!hasPickedWorld)
				checkLock3=true;
			if(Input.GetButtonDown("B Button")&&hasPickedWorld)
			{
				hasPickedWorld =false;
				checkLock4=true;
			}
			if(checkLock3)
			{
				hasPickedWorld = true;
				journeyLength = Vector3.Distance(selectedItem.transform.position, midPanel.transform.position);
				float distCovered = (Time.time - startTime) * mapMovementSpeed;
				float fracJourney = distCovered / journeyLength;
				selectedItem.transform.position = Vector3.Lerp(selectedItem.transform.position,midPanel.transform.position,fracJourney);
				selectedItem.gameObject.GetComponent<Animator>().SetBool("Maximize",true);
				for(int i = 0; i < mapSegments.Length; i++)
				{
					if(selectedItem!=mapSegments[i])
					{		
						mapSegments[i].gameObject.GetComponent<Animator>().SetBool("fade",true);
						//MapSegments[i].gameObject.GetComponent<RawImage>().CrossFadeAlpha(0f,0.1f,false);
						mapSegments[i].gameObject.GetComponent<Button>().enabled=false;
					}					
				}
				if(selectedItem.name=="BlueRaw")
					mapCams[0].restricted=true;
				if(selectedItem.name=="DesertRaw")
					mapCams[1].restricted=true;
				if(selectedItem.name=="MiniWorldRaw")
					mapCams[2].restricted=true;
				if(selectedItem.name=="MiscWorldRaw")
					mapCams[3].restricted=true;
				if(selectedItem.transform.position==midPanel.transform.position)
					checkLock3=false;
			}
			if(checkLock4)
			{
				if(selectedItem.name=="BlueRaw")
					savedPos=mapNulls[0];
				if(selectedItem.name=="DesertRaw")
					savedPos=mapNulls[1];
				if(selectedItem.name=="MiniWorldRaw")
					savedPos=mapNulls[2];
				if(selectedItem.name=="MiscWorldRaw")
					savedPos=mapNulls[3];
				journeyLength = Vector3.Distance(selectedItem.transform.position, savedPos.position);
				float distCovered = (Time.time - startTime) * mapMovementSpeed;
				float fracJourney = distCovered / journeyLength;
				selectedItem.transform.position = Vector3.Lerp(selectedItem.transform.position,savedPos.position,fracJourney);
				selectedItem.gameObject.GetComponent<Animator>().SetBool("Maximize",false);
				for(int i = 0; i < mapSegments.Length; i++)
				{
					if(selectedItem!=mapSegments[i])
					{		

						mapSegments[i].gameObject.GetComponent<Animator>().SetBool("fade",false);
						mapSegments[i].gameObject.GetComponent<Button>().enabled=true;
						//MapSegments[i].gameObject.GetComponent<RawImage>().CrossFadeAlpha(1f,2f,false);
					}
				}
				if(selectedItem.transform.position==savedPos.position)
					checkLock4=false;
			}
		}
		if(!toMap)
		{
			for(int i = 0; i < mapSegments.Length; i++)
			{
				mapSegments[i].gameObject.SetActive(false);
			}
			if(checkLock2)
			{
				gameM.mainCam.SetActive(false);
				gameM.mainCam.GetComponent<Camera>().enabled=false;
				gameM.mainCam.GetComponent<AudioListener>().enabled=false;
				gameM.player.SetActive(true);
				playerCam.SetActive(true);
				shipC.enabled=true;
				mainCC.enabled=true;
//				camFF.enabled=true;
//				customP.enabled=true;
				transform.position = playerCamPos.position;
			//	transform.rotation = ShipCamPos.rotation;
				transform.parent = ship.transform;
				ship.GetComponent<Rigidbody>().isKinematic = false;
				checkLock2=false;
			}
			if(Input.GetButtonDown("X Button")&&shipC.insideShip)
			{
				print("trying ToMap");
				toMap=true;
				checkLock1=true;
			}
		}
	}
	public void setButton()
	{
		hasPickedWorld = true;
		checkLock3 = true;
	}

}
