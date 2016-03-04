using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelTrigger: MonoBehaviour {

	
	//the canvas that the text is on 
	public GameObject uiCanvas;
	//the text that will get changed
	public Text LevelNamePopup;
	public GameMechanics gm;
	//the object that shows the level name on collision, make sure it has a trigger collider attached
	//the name of the level, duh
	public string LevelName;
	void Start()
	{
//		uiCanvas = this.gameObject.transform.FindChild ("Canvas").gameObject;
//		LevelNamePopup = uiCanvas.transform.FindChild ("Text").gameObject.GetComponent<Text> ();
	}
	void Awake()
	{
		if(gm==null)
			gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
	}
	public void OnTriggerEnter(Collider Hit)
	{
		//make sure to use this tag and that the collider is a trigger
		if(Hit.gameObject.tag == "Ship")
		{
			print ("HiUI");
			gm.selectedUI=uiCanvas;
			gm.selectedUI.GetComponent<PlanetUI>().textOn=true;
			LevelNamePopup.text = LevelName ;
		}
	}
	public void OnTriggerExit(Collider Hit)
	{
		if(Hit.gameObject.tag == "Ship")
		{
			gm.selectedUI.GetComponent<PlanetUI>().textOn=false;
		}
	}
}
