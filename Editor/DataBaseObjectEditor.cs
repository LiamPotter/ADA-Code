using UnityEngine;
using System.Collections;
using UnityEditor;


[CustomEditor(typeof(DataBaseObject))]
public class DataBaseObjectEditor : Editor {

    //This is the Editor Script for the DataBaseObject script
    //Creating this Editor Script allows for more flexibility in code
    //and makes it easier to quickly add new objects

    private int dietIndex;

    private int behaviourIndex;

    private int floraTypeIndex;

    //Will hide the Fauna options if 0, will display if 1
    private int hidingFaunaInt;
    //Will hide the Flora options if 0, will display if 1
    private int hidingFloraInt;
    //Will hide the Planet options if 0, will display if 1
    private int hidingPlanetInt;
    //The three Object Types
    private string[] objectTypes = new string[] { "Flora", "Fauna", "Planet" };
    //This int will be 0 for Flora, 1 for Fuana and 2 for Planet
    public int selectedObjectType;
    //The three different Diet Options 
    private string[] dietOptions = new string[] {"Omnivore","Herbivore","Carnivore"};
    //The three different Behaviour Options
    private string[] behaviourOptions = new string[] { "Passive", "Aggressive", "Docile" };
    //The three different Flora Types
    private string[] floraTypeOptions = new string[] { "Tree", "Flower", "Fungus" };
    private DataBaseObject thisDataBaseObject;
    void OnEnable()
    {
        thisDataBaseObject = (DataBaseObject)target;
        selectedObjectType = thisDataBaseObject.oObjectType;
        dietIndex = thisDataBaseObject.oDiet;
        behaviourIndex = thisDataBaseObject.oBehaviourNumber;
        floraTypeIndex = thisDataBaseObject.oFloraType;
    }
    public override void OnInspectorGUI()
    {
        
        thisDataBaseObject.oName=EditorGUILayout.TextField("Name",thisDataBaseObject.oName);
        thisDataBaseObject.oDescription = EditorGUILayout.TextField("Description", thisDataBaseObject.oDescription);
        selectedObjectType = EditorGUILayout.Popup("Object Type", selectedObjectType, objectTypes);

        //Sets the various bools based on the SelectedObjectType variable
        thisDataBaseObject.oObjectType = selectedObjectType;
        if (selectedObjectType != 0)
        {
            hidingFloraInt = 0;
        }
        else hidingFloraInt = 1;
        //This will hide the Diet Popup if the object is a plant, because plants do eat things... usually
        if (selectedObjectType!=1)
        {
            hidingFaunaInt = 0;
        }     
        else hidingFaunaInt = 1;
        if (selectedObjectType != 2)
        {
            hidingPlanetInt = 0;
        }
        else hidingPlanetInt = 1;
        if (EditorGUILayout.BeginFadeGroup(hidingFaunaInt))
        {
            dietIndex = EditorGUILayout.Popup("Diet", dietIndex, dietOptions);
            behaviourIndex = EditorGUILayout.Popup("Behaviour", behaviourIndex, behaviourOptions);
        }
        EditorGUILayout.EndFadeGroup();
        if (EditorGUILayout.BeginFadeGroup(hidingFloraInt))
        {
            floraTypeIndex = EditorGUILayout.Popup("Flora Type", floraTypeIndex, floraTypeOptions);
            thisDataBaseObject.oFlorahabitat = EditorGUILayout.TextField("Habitat", thisDataBaseObject.oFlorahabitat);
        }
        if (EditorGUILayout.BeginFadeGroup(hidingPlanetInt))
        {
            thisDataBaseObject.oPlanetLifeFormAmount = EditorGUILayout.IntField("Life Form Amount",thisDataBaseObject.oPlanetLifeFormAmount);
            thisDataBaseObject.oPlanetSignsOfIntelligence = EditorGUILayout.Toggle("Signs of Intelligence", thisDataBaseObject.oPlanetSignsOfIntelligence); 
        }
        EditorGUILayout.EndFadeGroup();
        thisDataBaseObject.oDiet = dietIndex;
        thisDataBaseObject.oBehaviourNumber = behaviourIndex;
        thisDataBaseObject.oFloraType = floraTypeIndex;

        EditorGUILayout.LabelField("Index Number", thisDataBaseObject.oIndexNumber.ToString());
        EditorGUILayout.LabelField("ObectType", thisDataBaseObject.oObjectType.ToString());
        thisDataBaseObject.oSeenByPlayer = EditorGUILayout.Toggle("Seen By Player?", thisDataBaseObject.oSeenByPlayer);
        base.OnInspectorGUI();
    }
   
}
