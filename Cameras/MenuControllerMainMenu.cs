using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuControllerMainMenu : MonoBehaviour {
	//This script needs to be placed on the camera in the scene
	public GameObject startText;
	public GameObject[] options;
	public GameObject pointer;
	public EventSystem eSystem;
	public bool hideStartText=false;

	void Update () 
	{
		if(Input.GetButtonUp("Pause")&&!hideStartText)
		{
			PressedStart();
		}
		if(hideStartText)
		{
			if(eSystem.currentSelectedGameObject!=null)
				pointer.transform.localPosition = new Vector3 (eSystem.currentSelectedGameObject.transform.localPosition.x-50f,eSystem.currentSelectedGameObject.transform.localPosition.y-45f,0);
		}
			
	}
	private void PressedStart()
	{
		startText.SetActive (false);
		for (int i = 0; i < options.Length; i++) 
		{
			if(options[i].activeSelf==false)
			{
				options[i].SetActive(true);
			}
		}
		hideStartText = true;
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
