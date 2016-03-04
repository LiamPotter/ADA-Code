using UnityEngine;
using System.Collections;

public class EnemyGravity : MonoBehaviour {
	public GravityAttractor planet;
	Rigidbody rigidbody;
	// Use this for initialization
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody> ();	
	}

	void OnTriggerEnter(Collider hit)
	{
		if(hit.gameObject.tag == "Activator")
		{
			planet= hit.transform.parent.gameObject.GetComponent<GravityAttractor>();
		}
	}
	// Update is called once per frame
	void FixedUpdate ()
	{
		if(planet!=null)
			planet.Attract(rigidbody);
	}
}
