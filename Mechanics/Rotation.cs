using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {


	public float rot = 1f;


	// Use this for initialization
	void Start () 
	{
//		GetComponent<Rigidbody>(rot)rigidbody.MoveRotation() = Quaternion.identity;
//		rb = GetComponent<Rigidbody>();


	}
	
	// Update is called once per frame
	void Update () 
	{
				transform.Rotate(Vector3.up, rot * Time.deltaTime);	
	}

	void FixedUpdate()
	{
//		rb.MoveRotation (Vector3.up, rot * Time.deltaTime);
//		Vector3 rotationVelocity = (Vector3.up)*rot*Time.deltaTime; 
//		rb.AddTorque (Vector3.up * rotationVelocity);

	}

}
