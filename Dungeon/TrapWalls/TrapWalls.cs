using UnityEngine;
using System.Collections;

public class TrapWalls : MonoBehaviour {
	public float timerToStart;
	public float timerToDrop;
	public float timerToRise;

	//storing timer variables
	private float tRise;
	private float tDrop;
	
	public bool moveUp;
	public bool moveDown;
	Animator anim;
	KillBoxKiller kbk;
	// Use this for initialization
	void Start () {
		kbk = FindObjectOfType<KillBoxKiller> ();
		anim = GetComponent<Animator> ();
		tRise = timerToRise;
		tDrop = timerToDrop;
	}
	
	// Update is called once per frame
	void Update () {
		if (timerToStart > 0) {
			timerToStart -= Time.deltaTime;
		}

		anim.SetFloat ("Start", timerToStart);

		if (timerToStart <= 0 && timerToStart >= -10) {
			MoveUp();
			timerToStart = -100;
		}

		if (moveUp) {
			timerToRise -= Time.deltaTime;
			anim.SetFloat("TimerToRise",timerToRise);
			if (timerToRise <= 0) {
				anim.SetBool("MoveUp",true);
				anim.SetBool("MoveDown",false);
				MoveDown();
				timerToRise = tRise;
			}
		}
		 if (moveDown) {
			timerToDrop -= Time.deltaTime;
			anim.SetFloat("TimerToDrop",timerToDrop);
			if (timerToDrop <= 0) {

				anim.SetBool("MoveDown",true);
				anim.SetBool("MoveUp",false);
				MoveUp();
				timerToDrop = tDrop;
			}
		}
	}

	void MoveDown()
	{
		moveDown = true;
		moveUp = false;


	}

	void MoveUp()
	{
		moveUp = true;
		moveDown = false;
	}

	void OnTriggerEnter(Collider c)
	{
		if (moveUp) {
			if (c.tag == "Player") {
			//Debug.Log("DEAD");
				kbk.isDead = true;
				kbk.TurnUIOn();
				//c.transform.position = new Vector3(0,0,0);
			}
		}

	}

}
