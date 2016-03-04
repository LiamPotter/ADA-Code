using UnityEngine;
using System.Collections;

public class Pillars : MonoBehaviour {
	public float fallSpeed;
	public float riseSpeed;
	public bool fall;
	public bool rise;
	public bool start;
	public bool stop;

	public Vector3 topPos;
	public Vector3 bottomPos;

	public float timerToDrop;
	public float timerToRise;


	private float tDrop;
	private float tRise;
	// Use this for initialization
	void Start () 
	{
		fall = false;
		rise = false;
		bottomPos.x = transform.position.x;
		bottomPos.z = transform.position.z;
		topPos.x = transform.position.x;
		topPos.z = transform.position.z;

		tDrop = timerToDrop;
		tRise = timerToRise;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!stop) 
		{

			if (rise) 
			{
				transform.position = Vector3.MoveTowards (transform.position, topPos, riseSpeed);

			}
			if (fall) 
			{
				transform.position = Vector3.MoveTowards (transform.position, bottomPos, fallSpeed);
			}
			if (start) 
			{

				if (transform.position == topPos)
				{
					rise = false;
					timerToDrop -= Time.deltaTime;
					if (timerToDrop <= 0) 
					{
						fall = true;
						timerToDrop = tDrop;
					}

				}

			}
		}

	}

	public void Reset()
	{
		transform.position = Vector3.MoveTowards(transform.position, bottomPos, fallSpeed);
		start = false;
		rise = false;
		fall = false;
		stop = true;

	}



}
