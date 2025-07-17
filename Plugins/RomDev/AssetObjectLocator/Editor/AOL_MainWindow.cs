using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
namespace RomDev.AssetObjectLocator
{
    public class AOL_MainWindow : EditorWindow
    {
        [SerializeField]
        private AOL_MainManagerSO aol_MainManagerSO;
        [MenuItem("Window/Asset Locator")]
        public static void Init()
        {
            AOL_MainWindow aolWindow = (AOL_MainWindow)GetWindow(typeof(AOL_MainWindow));
            aolWindow.titleContent.text = "Asset Locator";
        }
        private void OnInspectorUpdate()
        {
            Repaint();
        }
        private void OnGUI()
        {
            TryLoadAOLManagerSO();

            if (aol_MainManagerSO == null)
            {
                DrawEmptyAOLManager();
            }
            else
            {
                aol_MainManagerSO.ShowGUI();
            }
        }
        private void TryLoadAOLManagerSO()
        {
            if (aol_MainManagerSO != null)
            {
                return;
            }
            aol_MainManagerSO = Resources.Load<AOL_MainManagerSO>(AOL_Helper.DefaultAOLMainManagerName);
        }
        private void DrawEmptyAOLManager()
        {
            EditorGUILayout.LabelField("There's no AOL_MainManagerSO detected!!\nPlease create manually in Asset Object Locator's Resources Folder");
        }
    }
}

#endif
