using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(DataBaseMain))]
public class DataBaseMainEditor : Editor {
    //This is the Editor Script for the DataBaseMain script

    private bool floraOpen;
    private bool faunaOpen;
    private bool planetOpen;
    private DataBaseMain thisDataBaseMain;
    public override void OnInspectorGUI()
    {
        thisDataBaseMain = (DataBaseMain)target;
        GUILayout.Label("Database Objects");
        EditorGUI.indentLevel = 1;
        floraOpen =EditorGUILayout.Foldout(floraOpen, "Flora");
        if (floraOpen)
        {
            for (int i = 0; i < thisDataBaseMain.dObjectsFloraList.Count; i++)
            {
                EditorGUI.indentLevel = 2;
                EditorGUILayout.Foldout(false, thisDataBaseMain.dObjectsFloraList[i].oName);
                Debug.Log("testForFloraOpening");
            }
        }
        EditorGUI.indentLevel = 1;
        faunaOpen = EditorGUILayout.Foldout(faunaOpen, "Fuana");
        if (faunaOpen)
        {
            for (int i = 0; i < thisDataBaseMain.dObjectsFaunaList.Count; i++)
            {
                EditorGUI.indentLevel = 2;
                EditorGUILayout.Foldout(false, thisDataBaseMain.dObjectsFaunaList[i].oName);
                Debug.Log("testForFuanaOpening");
            }
        }
        EditorGUI.indentLevel = 1;
        planetOpen = EditorGUILayout.Foldout(planetOpen, "Planets");
        if(planetOpen)
        {
            for (int i = 0; i < thisDataBaseMain.dObjectsPlanetList.Count; i++)
            {
                EditorGUI.indentLevel = 2;
                EditorGUILayout.Foldout(false, thisDataBaseMain.dObjectsPlanetList[i].oName);
                Debug.Log("testForPlanetsOpening");
            }
        }

        if (GUILayout.Button("Find Database Objects"))
        {  
            DataBaseMain.FindAllDatabaseObjects(thisDataBaseMain.dObjectsList);
            DataBaseMain.SortAllDataBaseObjects(thisDataBaseMain.dObjectsList); 
            DataBaseMain.AddObjectsToSubLists(thisDataBaseMain.dObjectsList,thisDataBaseMain.dObjectsFloraList,thisDataBaseMain.dObjectsFaunaList,thisDataBaseMain.dObjectsPlanetList);
            DataBaseMain.SortAllDataBaseObjects(thisDataBaseMain.dObjectsFloraList);
            DataBaseMain.SortAllDataBaseObjects(thisDataBaseMain.dObjectsFaunaList);
            DataBaseMain.SortAllDataBaseObjects(thisDataBaseMain.dObjectsPlanetList);
            DataBaseMain.RemoveDuplicatesInDataBases(thisDataBaseMain.dObjectsList, thisDataBaseMain.dObjectsFloraList, thisDataBaseMain.dObjectsFaunaList, thisDataBaseMain.dObjectsPlanetList);
            floraOpen = true;
            faunaOpen = true;
            planetOpen = true;
            Debug.Log("HitButton");
        }
        if (GUILayout.Button("Instantiate UI"))
        {
            DataBaseMain.InstantiateUIElements(thisDataBaseMain.dObjectsList,thisDataBaseMain.dataBaseCanvasInvis);
        }
        if (GUILayout.Button("Reset UI"))
        {
            if (thisDataBaseMain.dataBaseCanvasInvis == null)
                thisDataBaseMain.dataBaseCanvasInvis = GameObject.Find("DatabaseUIPanelInvis");
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in thisDataBaseMain.dataBaseCanvasInvis.transform) children.Add(child.gameObject);
            children.ForEach(child => DestroyImmediate(child));
        }
        if (GUILayout.Button("Reset Everything"))
        {
            DataBaseMain.ResetAllDatabases(thisDataBaseMain.dObjectsList, thisDataBaseMain.dObjectsFloraList, thisDataBaseMain.dObjectsFaunaList, thisDataBaseMain.dObjectsPlanetList, thisDataBaseMain.dataBaseCanvasInvis);
        }
    }
   
}
