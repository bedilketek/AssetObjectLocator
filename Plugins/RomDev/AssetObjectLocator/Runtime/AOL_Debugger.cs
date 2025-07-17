using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace RomDev.AssetObjectLocator
{
    public static class AOL_Debugger
    {
        private static AOL_MainManagerSO AOL_ManagerSO
        {
            get
            {
                if (aol_ManagerSO == null)
                {
                    aol_ManagerSO = Resources.Load<AOL_MainManagerSO>(AOL_Helper.DefaultAOLMainManagerName);
                }
                return aol_ManagerSO;
            }
        }
        [NonSerialized]
        private static AOL_MainManagerSO aol_ManagerSO;
        public static void Debug(string message)
        {
            if (AOL_ManagerSO == null)
            {
                UnityEngine.Debug.Log(message);
                return;
            }

            if (AOL_ManagerSO.ShowRegularLog)
            {
                UnityEngine.Debug.Log(message);
            }
        }
        public static void DebugWarning(string message)
        {
            if (AOL_ManagerSO == null)
            {
                UnityEngine.Debug.LogWarning(message);
                return;
            }

            if (AOL_ManagerSO.ShowWarningLog)
            {
                UnityEngine.Debug.LogWarning(message);
            }
        }
        public static void DebugError(string message)
        {
            if (AOL_ManagerSO == null)
            {
                UnityEngine.Debug.LogError(message);
                return;
            }

            if (AOL_ManagerSO.ShowErrorLog)
            {
                UnityEngine.Debug.LogError(message);
            }
        }
    }
}

