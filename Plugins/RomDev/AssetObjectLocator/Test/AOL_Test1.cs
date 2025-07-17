using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace RomDev.AssetObjectLocator.Demo
{
    public class AOL_Test1 : MonoBehaviour
    {
#if UNITY_EDITOR
        private DefaultAsset folder;
        public void ShowGUI()
        {
            if (GUILayout.Button("Print Folder"))
            {
                PrintSelectedFolder();
            }
            ShowFolderField();
        }
        private void PrintSelectedFolder()
        {
            string path = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
            if (!string.IsNullOrEmpty(path))
            {
                Debug.Log(path);
            }
        }
        private void ShowFolderField()
        {
            EditorGUI.BeginChangeCheck();
            folder = (DefaultAsset)EditorGUILayout.ObjectField("Folder: ", folder, typeof(DefaultAsset), true);
            if (EditorGUI.EndChangeCheck())
            {
                if (folder != null)
                {
                    string path = AssetDatabase.GetAssetPath(folder);
                    if (!string.IsNullOrEmpty(path))
                    {
                        Debug.Log(path);
                    }
                }
            }
        }
        #endif
    }
}

