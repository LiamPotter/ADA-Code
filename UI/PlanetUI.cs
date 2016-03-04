using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlanetUI : MonoBehaviour {
	private GameMechanics gm;
	public GameObject ship;
	public Text thisText;
	public bool textOn;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gm==null)
			gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
		if(ship==null)
			ship = gm.ship;
		Vector3 relativePos = ship.transform.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation (relativePos,ship.transform.up);
		transform.rotation = rotation;
		if (textOn)
			thisText.enabled = true;
		else if (!textOn)
			thisText.enabled = false;

	}
}
