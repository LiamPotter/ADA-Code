using UnityEngine;
using System.Collections;

public class DataBaseObject:MonoBehaviour{

    //[HideInInspector]
    public GameObject oUIPrefab;

    public GameObject oUIObject;
    [HideInInspector]
    public int oObjectType;//0 for Flora, 1 for Fauna, 2 for Planet
    [HideInInspector]
    public string oName;
    [HideInInspector]
    public string oDescription;
    [HideInInspector]
    public int oIndexNumber;//Number in the database in alphabetical order
    [HideInInspector]
    public int oDiet;//0 for Omnivore, 1 for Herbivore, 2 for Carnivore
    [HideInInspector]
    public int oBehaviourNumber;//0 for Passive, 1 for Aggressive, 2 for Docile
    [HideInInspector]
    public string oFlorahabitat;
    [HideInInspector]
    public int oFloraType;
    [HideInInspector]
    public int oPlanetLifeFormAmount;
    [HideInInspector]
    public bool oPlanetSignsOfIntelligence;
    [HideInInspector]
    public bool oSeenByPlayer;//Whether or not the player has 'scanned' the object
    
 
  
}
