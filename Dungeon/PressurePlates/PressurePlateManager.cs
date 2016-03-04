using UnityEngine;
using System.Collections;

public class PressurePlateManager : MonoBehaviour {
	//Number of pressure plates to open the door
	public int numPressurePlates;
	//Different pressure plates;
	public GameObject[] pressurePlates;
	bool[] pressurePlatePressed;
	public bool openDoor;
	public GameObject door;
	// Use this for initialization

	void Start () {
		//pressurePlates = new GameObject[numPressurePlates];
		pressurePlatePressed = new bool[numPressurePlates];
	}
	
	// Update is called once per frame
	void Update () {
		if (openDoor) {
			door.GetComponent<Animator>().SetBool("Open",true);
		}
	}

	public void CheckPressurePlates()
	{
		for (int i = 0; i < numPressurePlates ; i++) {
			if (pressurePlates[i].GetComponent<PressurePlate>().pressed) {
				Debug.Log("OPEN");
				pressurePlatePressed[i] = true;
			}
			i++;
		}

		for (int j = 0; j < pressurePlatePressed.Length; j++) {
			if (pressurePlatePressed[j] == false) {
				openDoor = false;

			}
		
			j++;
		}

		for (int k = 0; k < pressurePlatePressed.Length; k++) {
			if (pressurePlatePressed[k] == true) {
				openDoor = true;
			}
		}
	}
}
