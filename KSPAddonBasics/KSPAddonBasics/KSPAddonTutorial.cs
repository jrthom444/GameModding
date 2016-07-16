using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPAddonBasics
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    class KSPAddonTutorial : MonoBehaviour
    {
        private void Log(string message)
        {
            Debug.Log("[KSPAddonTutorial]: [#!#]    " + message);
        }
        public KSPAddonTutorial()
        {
            Log("Constructor called");
        }
        void Awake()
        {
            Log("Awake Called");
        }
        void Start()
        {
            Log("Start Called");
        }
        void OnDestroy()
        {
            Log("Destroy Called");
        }
    }
}
