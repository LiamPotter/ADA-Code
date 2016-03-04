using UnityEngine;
using System.Collections;

public class PlanetoidActivator : MonoBehaviour
{
	public GameMechanics gm;
	public GameObject Player;
	public GameObject Planet;
	private GravityBody gravityBody;
	private GravityAttractor gravAttract;


	// Use this for initialization
	void Start () 
	{
//		Player = GameObject.FindGameObjectWithTag("Player")
		gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
		Player = gm.player;
		gravityBody = Player.GetComponent<GravityBody>();
//		gravAttract = Planet.GetComponent<GravityAttractor> ();
//		gravAttract = transform.parent.gameObject.GetComponent<GravityAttractor> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(gm==null)
			gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMechanics> ();
		if(Player==null)
			Player = gm.player;
		gravityBody = Player.GetComponent<GravityBody>();
	}

	void OnTriggerEnter(Collider whatHitMe)
	{
		gravAttract = transform.parent.gameObject.GetComponent<GravityAttractor> ();
		//gravityBody.enabled = !gravityBody.enabled;
		//gravAttract.enabled = !gravAttract.enabled;

	}

	void OnTriggerExit(Collider whatHitMe)
	{
		gravAttract = transform.parent.gameObject.GetComponent<GravityAttractor> ();
		//gravityBody.enabled = !gravityBody.enabled;
		//gravAttract.enabled = !gravAttract.enabled;

	}
}
