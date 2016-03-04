using UnityEngine;
using System.Collections;

public class LevelChangeTrigger : MonoBehaviour {
	//MUST BE EXACT
	public string LevelName;

	void OnTriggerEnter(Collider hit)
	{
		if (hit.tag == "Player")
		{
			Application.LoadLevel (LevelName);
			print ("HitLevelTrigger");
		}

	}
}
