using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {
	public GameMechanics gm;
	private MapCameraScript mapCamS;
	public GameObject planet;
	public bool restricted; 
	private Vector3 center;
	public float rotationSpeed;
	public float transitionSpeed;
	public float transitionTime;
	private bool rToSpawn=false;
	private float amount=1000f;
	public Transform[] spawnCamPos;
	public float closeDistance = 5.0F;
	void Start()
	{
		mapCamS = GameObject.Find ("MainCamera").GetComponent<MapCameraScript> ();
	}
	void Update ()
	{
		if(restricted)
		{
			center = planet.transform.position;
			if(!rToSpawn)
				transform.RotateAround(center,transform.up,Input.GetAxis("Horizontal")*rotationSpeed*Time.deltaTime);
			if(Input.GetButtonUp("Submit"))
			{
				rToSpawn=true;
			}
			if(rToSpawn)
				RotateToSpawn();
			transform.LookAt(center);
			if(Input.GetButtonUp("B Button"))
			{
				restricted=false;
			}
		}
	}
	public void RotateToSpawn()
	{

		print ("RotatingToSpawn");
		Vector3 offset = spawnCamPos[0].position - transform.position;
		float sqrLen = offset.sqrMagnitude;
		if (sqrLen < closeDistance * closeDistance)
		{
			print("The other transform is close to me!");
			transform.SetParent(planet.transform);
			rToSpawn=false;
		}
		transform.RotateAround(center, transform.up, rotationSpeed * Time.deltaTime);
//		if (distance <= 35)
//		{
//			rToSpawn = false;
//		}
//		Transform target = mapCamS.blueSpawnPoints[0].transform;
//		Vector3 targetDir = target.position - transform.position;
//		float step = rotationSpeed * Time.deltaTime;
//		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
//		Debug.DrawRay(transform.position, newDir, Color.red);
//		transform.rotation = Quaternion.LookRotation(newDir);
	}


}
	


