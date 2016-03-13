using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class UIObjectDropdown : MonoBehaviour {

    //These are the variables of the actual UI elements themselves, not the text that they hold
    public RectTransform infoContainer;

    public Text uIndexNumber;
    public Text uName;
    public Text uDescription;
    public Text uObjectType;
    //These two are the changable Extra Information blocks
    public Text uExtra1;
    public Text uExtra2;
    //These are the GameObjects of those two changable Extra Information Blocks
    public GameObject gExtra1;
    public GameObject gExtra2;
    private DataBaseMain dbMain;
    [HideInInspector]
    public bool foundAllElements=false;
    public bool isOpen;
    public bool completedMovement=true;
    public float moveTimeLeft;
    private float privateMoveTime;
    private float interactLockTime = 0.5f;
    private float iLockUse;
    public Vector3 posChange;
    public int thisIndexNumber;
    void Awake()
    {
        if(dbMain==null)
            dbMain = FindObjectOfType<DataBaseMain>();
        if(!foundAllElements)
            FindAllNeededUIElements();
        isOpen = false;
    }
    void Update()
    {
        ToggleContainer();
        if(iLockUse>0)
        {
            GetComponent<Button>().interactable = false;
            iLockUse -= Time.deltaTime;
        }
        else GetComponent<Button>().interactable = true;
        if (!completedMovement&&isOpen)
        {
            DataBaseMain.MoveAllUIElementsDown(dbMain.dObjectsList, this.gameObject,thisIndexNumber);
            privateMoveTime -= Time.deltaTime;
            if (privateMoveTime <= 0)
                completedMovement = true;
        }
        if (!completedMovement && !isOpen)
        {
            DataBaseMain.MoveAllUIElementsUp(dbMain.dObjectsList, this.gameObject, thisIndexNumber);
            privateMoveTime -= Time.deltaTime;
            if (privateMoveTime <= 0)
                completedMovement = true;
        }
       

    }
    public void OnButtonPress()
    {
        //This controls whether the dropdown is open or not
        privateMoveTime = moveTimeLeft;
        isOpen = !isOpen;
        completedMovement = false;
        iLockUse = interactLockTime;

    }
    public void ToggleContainer()
    {
        Vector3 scale = infoContainer.localScale;
        scale.y = Mathf.Lerp(scale.y, isOpen ? 1 : 0, Time.deltaTime * 18);
        infoContainer.localScale = scale;
    }
    public void FindAllNeededUIElements()
    {
        infoContainer = transform.FindChild("InfoContainer").GetComponent<RectTransform>();
        uIndexNumber = transform.FindChild("IndexLabel").GetComponent<Text>();
        uName = transform.FindChild("NameLabel").GetComponent<Text>();
        uDescription = infoContainer.transform.FindChild("Description").transform.GetChild(0).GetComponent<Text>();
        uObjectType = infoContainer.transform.FindChild("ObjectType").transform.GetChild(0).GetComponent<Text>();
        uExtra1 = infoContainer.transform.FindChild("ExtraInfo1").transform.GetChild(0).GetComponent<Text>();
        uExtra2 = infoContainer.transform.FindChild("ExtraInfo2").transform.GetChild(0).GetComponent<Text>();
        gExtra1 = infoContainer.transform.FindChild("ExtraInfo1").gameObject;
        gExtra2 = infoContainer.transform.FindChild("ExtraInfo2").gameObject;
        foundAllElements = true;
    }
    public RectTransform MoveElementDown(RectTransform toMove,int indexN)
    {
        int paddingInt = 100;
        posChange= toMove.localPosition;
        Vector3 targetPos = new Vector3(posChange.x, posChange.y - paddingInt);
        //positionChange.y = Mathf.Lerp(positionChange.y, amountToMove-paddingInt*indexN, Time.deltaTime * 3f);
        posChange = Vector3.Lerp(posChange, targetPos , Time.deltaTime * 11f);
        toMove.localPosition = posChange;
        return toMove;
    }
    public RectTransform MoveElementUp(RectTransform toMove, int indexN)
    {
        int paddingInt = 100;
        posChange = toMove.localPosition;
        Vector3 targetPos = new Vector3(posChange.x, posChange.y + paddingInt);
        //positionChange.y = Mathf.Lerp(positionChange.y, amountToMove-paddingInt*indexN, Time.deltaTime * 3f);
        posChange = Vector3.Lerp(posChange, targetPos, Time.deltaTime * 11f);
        toMove.localPosition = posChange;
        return toMove;
    }

}
