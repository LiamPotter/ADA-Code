using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour 
{

	public float gravity = -9.8f;
	public bool interior;
	public Vector3 gravityUp;
	void Update()
	{

	}

	public void Attract(Rigidbody body) 
	{
		gravityUp = (body.position - transform.position).normalized;
		Vector3 localUp = body.transform.up;
		// Apply downwards gravity to body
		body.AddForce (gravityUp * gravity);
		// Allign bodies up axis with the centre of planet
//		if(!interior)
//			body.rotation = Quaternion.FromToRotation (localUp, gravityUp) * body.rotation;	
	}  
}
