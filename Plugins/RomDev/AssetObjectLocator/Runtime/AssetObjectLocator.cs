using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RomDev.AssetObjectLocator
{
    public static class AssetObjLocator
    {
        public static AOL_MainManagerSO MainManagerSO
        {
            get
            {
                if (mainManagerSO == null)
                {
                    mainManagerSO = Resources.Load<AOL_MainManagerSO>(AOL_Helper.DefaultAOLMainManagerName);
                }
                return mainManagerSO;
            }
        }
        private static readonly AOL_VersionData currentAssetLocaterVer = new(1,0,0);
        [NonSerialized]
        private static AOL_MainManagerSO mainManagerSO;
        public static bool GetIDOfObject(UnityEngine.Object unityObj, out int objID)
        {
            if (MainManagerSO.GetObjectID(unityObj, out objID))
            {
                return true;
            }
            return false;
        }
        public static bool GetObjectByID(int objID, out UnityEngine.Object unityObj)
        {
            if (MainManagerSO.GetObjectByID(objID, out unityObj))
            {
                return true;
            }
            return false;
        }
    }
}

