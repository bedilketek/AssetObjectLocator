using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif
namespace RomDev.AssetObjectLocator
{
    [Serializable]
    public class FolderBlock
    {
        public string BlockName
        {
            get
            {
                return blockName;
            }
        }
        [SerializeField]
        private string blockName = "Unknown";
        [SerializeField]
        private AOL_MainManagerSO mainManagerSO;
        [SerializeField]
        private AOL_DictionaryBridge<int, UnityEngine.Object> objectDictBridge = new();
        [SerializeField]
        private AOL_DictionaryBridge<UnityEngine.Object, int> objectDictReverse = new();
        [SerializeField]
        private List<AOL_EnumVault.UnityObjectType> targetTypes = new();
#if UNITY_EDITOR
        [SerializeField]
        private DefaultAsset folderAsset;
        [SerializeField]
        private string folderPath;
        [SerializeField]
        private bool showTargetTypes;
        private Vector2 scrollObjList;
        public FolderBlock(AOL_MainManagerSO _mainManagerSO)
        {
            mainManagerSO = _mainManagerSO;
        }
        public void ShowGUI()
        {
            EditorGUILayout.LabelField("Block : " + blockName);
            blockName = EditorGUILayout.TextField("Name: ", blockName);
            DrawFolderField();
            if (!string.IsNullOrEmpty(folderPath))
            {
                EditorGUILayout.LabelField("Path:" + folderPath);
            }
            DrawTargetTypes();
            if (!string.IsNullOrEmpty(folderPath))
            {
                if (GUILayout.Button("Refresh And Gather Assets"))
                {
                    FindDatasInsideFolder();
                }
            }
            DrawObjList();
        }
        private void DrawFolderField()
        {
            EditorGUI.BeginChangeCheck();
            folderAsset = (DefaultAsset)EditorGUILayout.ObjectField("Folder: ", folderAsset, typeof(DefaultAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                if (folderAsset != null)
                {
                    string path = AssetDatabase.GetAssetPath(folderAsset);
                    if (string.IsNullOrEmpty(path))
                    {
                        AOL_Debugger.DebugWarning("Path : " + path + "\nDoesnt Exist!");
                    }
                    else
                    {
                        folderPath = path;
                    }
                }
                else
                {
                    folderPath = "";
                }
            }
        }
        private void DrawTargetTypes()
        {
            showTargetTypes = EditorGUILayout.Foldout(showTargetTypes, "Target Types");
            if (!showTargetTypes)
            {
                return;
            }
            for (int i = 0; i < targetTypes.Count; i++)
            {
                bool deleteData = false;
                Rect thisRect = EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(i + ":", GUILayout.MinWidth(10), GUILayout.MaxWidth(30));
                targetTypes[i] = (AOL_EnumVault.UnityObjectType)EditorGUILayout.EnumPopup(targetTypes[i]);
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    deleteData = true;
                }
                EditorGUILayout.EndHorizontal();
                if (deleteData)
                {
                    targetTypes.RemoveAt(i);
                    break;
                }
            }
            if (GUILayout.Button("Add Type"))
            {
                targetTypes.Add(AOL_EnumVault.UnityObjectType.GameObject);
            }
        }
        private void FindDatasInsideFolder()
        {
            mainManagerSO.RefreshCache();
            objectDictBridge.Clear();
            objectDictReverse.Clear();
            List<UnityEngine.Object> foundedObjects = new();

            // Filtering Objects
            foreach (AOL_EnumVault.UnityObjectType targetType in targetTypes)
            {
                string filterName = AOL_Helper.GetStringOfUnityTypeAsset(targetType);
                Type type = AOL_Helper.GetTypeOfUnityTypeObject(targetType);
                string[] guids = AssetDatabase.FindAssets($"t:{filterName}", new[] { folderPath });
                foreach (string guid in guids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    UnityEngine.Object loadedObj = AssetDatabase.LoadAssetAtPath(path, type);
                    if (foundedObjects.Contains(loadedObj))
                    {
                        continue;
                    }
                    foundedObjects.Add(loadedObj);
                }
            }

            // Adding to dictionary
            foreach (UnityEngine.Object foundedObject in foundedObjects)
            {
                mainManagerSO.AddObjectCache(foundedObject, out int objID);
                objectDictBridge.AddData(objID, foundedObject);
                objectDictReverse.AddData(foundedObject, objID);
            }
        }
        private void DrawObjList()
        {
            scrollObjList = GUILayout.BeginScrollView(scrollObjList, GUILayout.MinHeight(50), GUILayout.MaxHeight(200));
            for (int i = 0; i < objectDictBridge.Values.Count; i++)
            {
                int objID = objectDictBridge.Keys[i];
                bool deleteData = false;
                UnityEngine.Object currentObj = objectDictBridge.Values[i];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("ID:" + objID, GUILayout.MinWidth(60), GUILayout.MaxWidth(100));
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.ObjectField(currentObj, typeof(UnityEngine.Object), false);
                }
                if (GUILayout.Button("-", GUILayout.Width(50)))
                {
                    deleteData = true;
                }
                EditorGUILayout.EndHorizontal();
                if (deleteData)
                {
                    objectDictBridge.RemoveData(objID);
                    break;
                }
            }
            GUILayout.EndScrollView();
        }
#endif
        public bool GetObjectByID(int objID, out UnityEngine.Object resultObj)
        {
            resultObj = null;
            if (objectDictBridge.ContainsKey(objID))
            {
                resultObj = objectDictBridge.GetData(objID);
                return true;
            }
            return false;
        }
        public bool GetIDOfObject(UnityEngine.Object resultObj, out int objID)
        {
            objID = -1;
            if (objectDictReverse.ContainsKey(resultObj))
            {
                objID = objectDictReverse.GetData(resultObj);
                return true;
            }
            return false;
        }
    }
}

