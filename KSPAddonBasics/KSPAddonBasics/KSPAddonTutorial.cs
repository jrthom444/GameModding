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

            GameEvents.onGameSceneSwitchRequested.Add(onSceneSwitchRequest);
            DontDestroyOnLoad(this);
        }
        void Start()
        {
            Log("Start Called");
        }
        void OnDestroy()
        {
            Log("Destroy Called");
        }

        public void onSceneSwitchRequest(GameEvents.FromToAction<GameScenes,GameScenes> action){
            Log("Scene Changed from " + action.from + " to " + action.to);
        }

    }
}
