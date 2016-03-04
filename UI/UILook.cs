using UnityEngine;
using System.Collections;

public class UILook : MonoBehaviour {

	public GameObject lookTarget;
	void Update () 
	{
			Vector3 relativePos = lookTarget.transform.position - transform.position;
			Quaternion rotation = Quaternion.LookRotation (-relativePos,lookTarget.transform.up);
			//Quaternion rotation = Quaternion.LookRotation (-Vector3.forward);
			transform.rotation = rotation;
	
	}
}
