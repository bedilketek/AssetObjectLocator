using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RomDev.AssetObjectLocator
{
    [Serializable]
    public class AOL_VersionData
    {
        public int MajorVersion
        {
            get
            {
                return majorVersion;
            }
        }
        public int MinorVersion
        {
            get
            {
                return minorVersion;
            }
        }
        public int PatchVersion
        {
            get
            {
                return patchVersion;
            }
        }
        public string VersionName
        {
            get
            {
                return majorVersion + "." + minorVersion + "." + patchVersion;
            }
        }
        [SerializeField]
        private int majorVersion;
        [SerializeField]
        private int minorVersion;
        [SerializeField]
        private int patchVersion;
        public AOL_VersionData(int _majorVersion, int _minorVersion, int _patchVersion)
        {
            majorVersion = _majorVersion;
            minorVersion = _minorVersion;
            patchVersion = _patchVersion;
        }
    }
}

