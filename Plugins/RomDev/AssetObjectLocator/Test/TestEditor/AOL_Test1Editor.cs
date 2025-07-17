

#if UNITY_EDITOR
using UnityEditor;

namespace RomDev.AssetObjectLocator.Demo
{
    [CustomEditor(typeof(AOL_Test1))]
    public class AOL_Test1Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            AOL_Test1 aOL_Test1 = (AOL_Test1)target;
            aOL_Test1.ShowGUI();
        }
    }
}

#endif
