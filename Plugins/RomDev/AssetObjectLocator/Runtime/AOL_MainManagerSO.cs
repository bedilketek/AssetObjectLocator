using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace RomDev.AssetObjectLocator
{
    [CreateAssetMenu(fileName = "AOL_MainManagerSO", menuName = "AOL/AOL_MainManagerSO", order = 1)]
    public class AOL_MainManagerSO : ScriptableObject
    {
        // Debugger
        public bool ShowRegularLog
        {
            get
            {
                return showRegularLog;
            }
        }
        public bool ShowWarningLog
        {
            get
            {
                return showWarningLog;
            }
        }
        public bool ShowErrorLog
        {
            get
            {
                return showErrorLog;
            }
        }
        [SerializeField]
        private bool showRegularLog;
        [SerializeField]
        private bool showWarningLog;
        [SerializeField]
        private bool showErrorLog;
        [SerializeField]
        private List<FolderBlock> folderBlocks = new();
#if UNITY_EDITOR
        [SerializeField]
        private AOL_DictionaryBridge<UnityEngine.Object, int> cachedObjectBridge = new();
        private Vector2 scrollFolderBlock;
        [NonSerialized]
        private FolderBlock selectedBlock;
        public void ShowGUI()
        {
            EditorGUI.BeginChangeCheck();
            DrawGeneralGUI();
            DrawFolderBlocks();
            DrawSelectedBlock();
            if(EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(this);
            }
        }
        public void AddObjectCache(UnityEngine.Object unityObj, out int resultID)
        {
            resultID = 0;
            if (cachedObjectBridge.Keys.Contains(unityObj))
            {
                resultID = cachedObjectBridge.GetData(unityObj);
                return;
            }
            resultID = unityObj.GetInstanceID();
            cachedObjectBridge.AddData(unityObj, resultID);
        }
        public void RefreshCache()
        {
            for (int i = 0; i < cachedObjectBridge.Keys.Count; i++)
            {
                if (cachedObjectBridge.Keys[i] == null)
                {
                    cachedObjectBridge.RemoveDataByIndex(i);
                }
            }
        }
        private void DrawFolderBlocks()
        {
            EditorGUILayout.LabelField("Target Folders");
            scrollFolderBlock = GUILayout.BeginScrollView(scrollFolderBlock, GUILayout.MinHeight(50), GUILayout.MaxHeight(200));
            for (int i = 0; i < folderBlocks.Count; i++)
            {
                bool deleteData = false;
                string buttonText = folderBlocks[i].BlockName;
                if (selectedBlock != null)
                {
                    if (selectedBlock == folderBlocks[i])
                    {
                        buttonText += "_(Selected)";
                    }
                }
                EditorGUILayout.BeginHorizontal();
                if (GUILayout.Button(buttonText))
                {
                    selectedBlock = folderBlocks[i];
                }
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    deleteData = true;
                }
                EditorGUILayout.EndHorizontal();
                if (deleteData)
                {
                    if (selectedBlock != null)
                    {
                        if (selectedBlock == folderBlocks[i])
                        {
                            selectedBlock = null;
                        }
                    }
                    folderBlocks.RemoveAt(i);
                    break;
                }
            }
            GUILayout.EndScrollView();
            if (GUILayout.Button("Add Target Folder"))
            {
                FolderBlock folderBlock = new(this);
                folderBlocks.Add(folderBlock);
            }
        }
        private void DrawSelectedBlock()
        {
            if (selectedBlock == null) return;
            EditorGUILayout.Space();
            selectedBlock.ShowGUI();
        }
        private void ClearCache()
        {
            cachedObjectBridge.Clear();
        }
        private void DrawGeneralGUI()
        {
            if (GUILayout.Button("Clear Cache"))
            {
                ClearCache();
            }
        }
#endif
        public bool GetObjectID(UnityEngine.Object unityObj, out int objID)
        {
            objID = -1;
            foreach (FolderBlock folderBlock in folderBlocks)
            {
                if (folderBlock.GetIDOfObject(unityObj, out objID))
                {
                    return true;
                }
            }
            return false;
        }
        public bool GetObjectByID(int objID, out UnityEngine.Object resultObj)
        {
            resultObj = null;
            foreach (FolderBlock folderBlock in folderBlocks)
            {
                if (folderBlock.GetObjectByID(objID, out resultObj))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

