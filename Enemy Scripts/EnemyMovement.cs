using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	float speed= 2;
	Vector3 wayPoint ;
	Vector3 oldWayPoint;
	int Range= 3;
	GameObject player;
	
	float moveSpeed = 3; 
	float rotationSpeed = 3;
	float distanceFromTarget  = 3f;
	private float _currentDistance = 0;
	void Start(){
	
		//initialise the target way point
		Wander();
	}
	
	void Update() 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		// this is called every frame
		// do move code here
		if (player != null) {
			if (wayPoint != null && Vector3.Distance (transform.position, player.transform.position) > 10) {
			
			
				transform.position += transform.TransformDirection (Vector3.forward) * speed * Time.deltaTime;
				if ((transform.position - wayPoint).magnitude < 3) {
					// when the distance between us and the target is less than 3
					// create a new way point target
					Wander ();
				}
			} else {
				_currentDistance = Vector3.Distance (player.transform.position, this.transform.position);
			
				//put inside if statement if you want the enemy to stop looking at the target also
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (player.transform.position - this.transform.position), rotationSpeed * Time.deltaTime);
			
				if (distanceFromTarget <= _currentDistance) {
					this.transform.position += this.transform.forward * moveSpeed * Time.deltaTime;
				}
			
			}
		}
	}
	
	void Wander()
	{ 
		// does nothing except pick a new destination to go to
		oldWayPoint = wayPoint;
		do {
			wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), 1, Random.Range(transform.position.z - Range, transform.position.z + Range));
		} while (Vector3.Distance(oldWayPoint,wayPoint) < 3);
		
		wayPoint.y = 1;
		// don't need to change direction every frame seeing as you walk in a straight line only
		transform.LookAt(wayPoint);
		//Debug.Log(wayPoint + " and " + (transform.position - wayPoint).magnitude);
	}
}
