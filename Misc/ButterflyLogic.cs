using UnityEngine;
using System.Collections;

public class ButterflyLogic : MonoBehaviour {
	float speed= 1f;
	Vector3 wayPoint ;
	Vector3 oldWayPoint;
	int Range= 3;

	private Vector3 startPos;	
		
	float moveSpeed = 0.5f; 
	float rotationSpeed = 1;
	float distanceFromTarget  = 3f;
	private float _currentDistance = 0;
	private float timer = 5;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		startPos.y = 1;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		//Debug.Log (Vector3.Distance (wayPoint, startPos));
//		if (wayPoint != null) {
//			transform.position += transform.TransformDirection (Vector3.forward) * speed * Time.deltaTime;
//			if ((transform.position - wayPoint).magnitude < 3) {
//				// when the distance between us and the target is less than 3
//				// create a new way point target
//				Wander ();
//			}
//		}

		if (timer > 0) {
			transform.position += transform.TransformDirection (-Vector3.forward) * speed * Time.deltaTime;
		}
		else {
			transform.Rotate(Vector3.Lerp(transform.position,new Vector3(0,Random.Range(45,180),0), 10));
			timer = Random.Range(3,6);
		}

	}

	void Wander()
	{ 

		// does nothing except pick a new destination to go to
		oldWayPoint = wayPoint;
		do {
			wayPoint = new Vector3(Random.Range(startPos.x - Range, startPos.x + Range), 1, Random.Range(startPos.z - Range, startPos.z + Range));
		} while (Vector3.Distance(oldWayPoint,wayPoint) > 3 );
		
		wayPoint.y = 1;
		// don't need to change direction every frame seeing as you walk in a straight line only
		transform.LookAt (wayPoint);
		//Debug.Log(wayPoint + " and " + (transform.position - wayPoint).magnitude);
	}

	void OnCollisionEnter(Collision c)
	{
		if (c.gameObject.tag == "Butterfly") {
			Physics.IgnoreCollision(GetComponent<Collider>(), c.collider);
		}
	}




}
