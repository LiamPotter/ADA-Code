using UnityEngine;
using System.Collections;

public class Tweening : MonoBehaviour {

//	public GameMechanics gm;
//	public MainCameraController mCC;
//	public Transform[] planetSplinePath;
//	public Color planetSplinePathColor;
//	public float percentsPerSecond = 0.2f; // % of the path moved per second
//	public float currentPathPercent = 0.0f; //min 0, max 1
//	public bool check=false;
//	public bool tweenInProgress;
//	private Transform closestCP;
//	void Awake()
//	{
//		mCC = GetComponent<MainCameraController> ();
//	}
//	void Update()
//	{
//		if(!check)
//		{
//			currentPathPercent =0f;
//			check=true;
//		}
//		if(gm.selectedPlanet!=null)
//		{
//			planetSplinePath [1] = gm.shipSC.closestPoint;
//			planetSplinePath [2] = gm.spawnSC.closestPoint;
//		}
//		if (mCC.tweenEnd)
//			check = false;
//		
//	}
//
//	public void TweenBetween(Transform start,Transform target)
//	{
//
//		if(currentPathPercent<1)
//		{
//			currentPathPercent += percentsPerSecond * Time.deltaTime;
//		}
//		if (currentPathPercent > 1)
//		{
//			currentPathPercent = 1;
//		}
//		iTween.RotateUpdate (gameObject, target.rotation.eulerAngles, 2f);
//		planetSplinePath [0] = start;
//
//		planetSplinePath [3] = target;
//		iTween.PutOnPath(gameObject,planetSplinePath,currentPathPercent);
//		//iTween.MoveUpdate(gameObject,iTween.Hash("path",planetSplinePath,"time",1.5));
//		tweenInProgress = true;
//		print ("Tween In Progress");
//	}
////	Transform GetClosestCheckPoint(Transform[] checkpoints)
////	{
////		Transform tMin = null;
////		float minDist = Mathf.Infinity;
////		Vector3 currentPos = transform.position;
////		foreach (Transform t in checkpoints)
////		{
////			float dist = Vector3.Distance(t.position, currentPos);
////			if (dist < minDist)
////			{
////				tMin = t;
////				minDist = dist;
////			}
////		}
////		return tMin;
////	}
//	void OnDrawGizmos()
//	{
//		iTween.DrawPathGizmos(planetSplinePath,planetSplinePathColor);
//	}
}
