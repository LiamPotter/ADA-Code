
using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour {


	//set gravity to planet you are in orbit of
	
	//parent player to that planet
	public GameMechanics gm;
	public GravityAttractor planet;
	Rigidbody rigidbody;

	void Awake () 
	{
		//gm = GameObject.FindWithTag ("GameController").GetComponent<GameMechanics> ();
		rigidbody = GetComponent<Rigidbody> ();	
		gm=GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
// 		Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
//		rigidbody.useGravity = false;
//		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}
	void Update()
	{

	}
	
	void FixedUpdate () 
	{

		if(gm.selectedPlanet!=null&&!gm.ship.GetComponent<ShipController>().insideShip)
			planet = gm.selectedPlanet.GetComponent<GravityAttractor>();
		if(gm.insideShip&&planet!=gm.ship.GetComponent<ShipController>().gravityWell.GetComponent<GravityAttractor>())
		{	
			//planet=gm.selectedPlanet.GetComponent<GravityAttractor>();
			planet = gm.ship.GetComponent<ShipController>().gravityWell.GetComponent<GravityAttractor>();
		}
		// Allow this body to be influenced by planet's gravity
		if(gm.playerCam&&this.tag!="Butterfly")
			planet.Attract(rigidbody);
	
	}
}