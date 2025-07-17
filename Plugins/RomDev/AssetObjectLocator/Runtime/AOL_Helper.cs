using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomDev.AssetObjectLocator
{
    public static class AOL_Helper
    {
        public const string DefaultAOLMainManagerName = "AOL_MainManagerSO";
        public static T DeepCopy<T>(T obj)
        {
            string json = JsonUtility.ToJson(obj);
            return JsonUtility.FromJson<T>(json);
        }
        public static Type GetTypeOfUnityTypeObject(AOL_EnumVault.UnityObjectType unityObjectType)
        {
            Type type = null;
            switch (unityObjectType)
            {
                case AOL_EnumVault.UnityObjectType.GameObject:
                    type = typeof(GameObject);
                    break;
                case AOL_EnumVault.UnityObjectType.ScriptableObject:
                    type = typeof(ScriptableObject);
                    break;
                case AOL_EnumVault.UnityObjectType.Material:
                    type = typeof(Material);
                    break;
                case AOL_EnumVault.UnityObjectType.Texture:
                    type = typeof(Texture2D);
                    break;
                case AOL_EnumVault.UnityObjectType.Sprite:
                    type = typeof(Sprite);
                    break;
            }
            return type;
        }
        public static string GetStringOfUnityTypeAsset(AOL_EnumVault.UnityObjectType unityObjectType)
        {
            string assetStringType = "";
            switch (unityObjectType)
            {
                case AOL_EnumVault.UnityObjectType.GameObject:
                    assetStringType = "Prefab";
                    break;
                case AOL_EnumVault.UnityObjectType.ScriptableObject:
                    assetStringType = "ScriptableObject";
                    break;
                case AOL_EnumVault.UnityObjectType.Material:
                    assetStringType = "Material";
                    break;
                case AOL_EnumVault.UnityObjectType.Texture:
                    assetStringType = "Texture";
                    break;
                case AOL_EnumVault.UnityObjectType.Sprite:
                    assetStringType = "Sprite";
                    break;
            }
            return assetStringType;
        }
    }
}

