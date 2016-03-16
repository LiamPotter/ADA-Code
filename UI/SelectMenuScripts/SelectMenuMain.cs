using UnityEngine;
using System.Collections;

public class SelectMenuMain : MonoBehaviour {

    public int selectedTab; //the int value that changes on bumper press
    public GameObject currentTab; //this is changed depending the selectedTab int
    public GameObject objectivesTab, encyclopediaTab, mapTab; //these are the three actual ui objects in the world space
    void Start()
    {
        //setting the selected object at the start of the scene
        selectedTab = 0;
        setCurrentTab();
    }

    void Update()
    {
        //decreasing the selectedTab int by one to cycle left through the menus
        if (Input.GetButtonDown("LeftBumper"))
        {
            if (selectedTab != 0)
                selectedTab = selectedTab - 1;
            else selectedTab = 2;
            setCurrentTab();
        }
        //increasing the selectedTab int by one to cycle right through the menus
        if (Input.GetButtonDown("RightBumper"))
        {
            if (selectedTab != 2)
                selectedTab = selectedTab + 1;
            else selectedTab = 0;
            setCurrentTab();
        }
    }
    //this is the static method in charge of setting each objects position in the heirarchy
    public static void MoveInHeirarchy(int delta, Transform toMove)
    {
        int index = toMove.GetSiblingIndex();
        toMove.SetSiblingIndex(index + delta);
    }
    //this is the public method that syncs the selectedTab int variable to the currentTab GameObject variable
    public void setCurrentTab()
    {
        switch (selectedTab)
        {
            case 2:
                currentTab = mapTab;
                MoveInHeirarchy(3, currentTab.transform);
                break;
            case 1:
                currentTab = encyclopediaTab;
                MoveInHeirarchy(3, currentTab.transform);
                break;
            default:
                currentTab = objectivesTab;
                MoveInHeirarchy(3, currentTab.transform);
                break;                         
        }
    }
}
