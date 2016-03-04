using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Optimization : MonoBehaviour {
	GameObject Player;
	public float radius;
	private GameObject[] environmentalObjects;
	public int optimizeIndex;
	// Use this for initialization
	void Start () {
	
		Player = GameObject.FindGameObjectWithTag ("Player");
		environmentalObjects = GameObject.FindGameObjectsWithTag ("Environmental");


	}
	
	// Update is called once per frame
	void Update () {

		if (optimizeIndex == 1) {
			for (int i = 0; i < environmentalObjects.Length; i++) {
				if (Vector3.Distance(Player.transform.position, environmentalObjects[i].transform.position) > 10) {
					try {
						environmentalObjects[i].GetComponent<Collider>().enabled = false;
					} catch (MissingComponentException ex) {
					//	Debug.Log("There is no collider attatched to " + environmentalObjects[i].name);
					}
			
				}
				else {
					try {
						environmentalObjects[i].GetComponent<Collider>().enabled = true;
					} catch (MissingComponentException ex) {
						
					}
			
				}
				i++;
			}
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (optimizeIndex == 2) {
			if (other.gameObject.tag == "Environmental") {
				other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
			}
		
		}

	}
	void OnTriggerExit(Collider other)
	{
		if (optimizeIndex == 2) {
			if (other.gameObject.tag == "Environmental") {
				other.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
			}

		}

	}


}
