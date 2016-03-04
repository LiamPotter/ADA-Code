using UnityEngine;
using System.Collections;

public class LODController : MonoBehaviour {

	public float[] distanceRanges;
	public GameObject[] lodModels;
	private int current =-2;

	// Use this for initialization
	void Start () 
	{
		for (int i=0; i<lodModels.Length; i++)
		{
			lodModels[i].SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		float d = Vector3.Distance (Camera.main.transform.position, transform.position);
		int level = -1;
		for(int i=0;i<distanceRanges.Length;i++)
		{
			if(d<distanceRanges[i])
			{
				level = i;
				i= distanceRanges.Length;
			}
		}
		if(level==-1)
		{
			level = distanceRanges.Length;
		}
		if(current!=level)
		{
			ChangeLOD(level);
		}
	}
	public void ChangeLOD(int level)
	{
		lodModels [level].SetActive (true);
		if(current>= 0)
		{
			lodModels [current].SetActive (false);
		}
		current = level;
	}
}
