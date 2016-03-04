using UnityEngine;
using System.Collections;

public class TrapButton : MonoBehaviour {
	public string trapName;
	public bool buttonPressed;
	public GameObject[] traps;
	public bool stop;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (stop) {
			foreach (GameObject trap in traps) {
				trap.GetComponent<Pillars>().Reset();
			}
		}
	}

	void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Player") {
			if (trapName.ToUpper() == "FALLING PILLAR") {
				stop = false;
				foreach (GameObject trap in traps) {
					trap.GetComponent<Pillars>().rise = true;
					trap.GetComponent<Pillars>().fall = false;
					trap.GetComponent<Pillars>().start = true;
					trap.GetComponent<Pillars>().stop = false;
					buttonPressed = true;
				}
			}
			if (trapName.ToUpper() == "OPEN DOOR") {
				traps[0].GetComponent<Animator>().SetBool("Open",true);
			}
		}

	}

	void OnTriggerExit(Collider c)
	{


	}
}
