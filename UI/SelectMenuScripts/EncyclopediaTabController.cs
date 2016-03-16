using UnityEngine;
using System.Collections;

public class EncyclopediaTabController : MonoBehaviour {

    public SelectMenuMain selectMM;
    public DataBaseMain dbMain;
    public RectTransform currentUIPanel;
	void Start ()
    {
        if (selectMM == null)
            selectMM = transform.parent.GetComponent<SelectMenuMain>();
        if (dbMain == null)
            dbMain = FindObjectOfType<DataBaseMain>();
	}
		
	void Update ()
    {
        if (selectMM.currentTab == this.gameObject)
        {
            MoveUIPanel(currentUIPanel, Input.GetAxis("RightStickY"), selectMM, dbMain);
        }
	}
    public static void MoveUIPanel(RectTransform uipanel, float direction, SelectMenuMain smm,DataBaseMain dbm)//uipanel is the object that will move, if direction is 0 move up, if 1 move down
    {
        
    }
}
