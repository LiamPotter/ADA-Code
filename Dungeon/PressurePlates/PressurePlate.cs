using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {
	public bool pressed;
	bool openDoor;
	PressurePlateManager ppm;
	// Use this for initialization
	void Start () {
		pressed = false;
		ppm = FindObjectOfType<PressurePlateManager> ();
	}

	void OnTriggerEnter(Collider c)
	{
		//can change tag to be a box or soemthing
		if (c.gameObject.tag == "Player") {
			//Debug.Log("INTHISSECTION");
			pressed = true;
			ppm.CheckPressurePlates();
			//animate the pressure plate down
		}
	}
	void OnTriggerExit(Collider c)
	{
		//animate the pressure pad up
		pressed = false;
	}
}
