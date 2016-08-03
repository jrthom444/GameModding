using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPAddonBasics
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    class KSPAddonTutorial : MonoBehaviour
    {
        private DebugWindow Logger;

        public KSPAddonTutorial()
        {
            //Use KSP's built-in logger to show initialization.
            Debug.Log("[KSPAddonTutorial]:Addon Initialized");
        }
        void Awake()
        {
            //Create a Unity IMGUI.GUILayout window designed to show debug statements.
            Logger = new DebugWindow(new WindowDimensions { Top = 0, Left = 0, Height = 300, Width = 500 });   
            GameEvents.onGameSceneSwitchRequested.Add(onSceneSwitchRequest);
            DontDestroyOnLoad(this);
        }
        void Start()
        {
            Logger.Info("Start Called: Window Initialized: " + Logger.WindowName);
        }
        void OnDestroy()
        {
            Debug.Log("[KSPAddonTutorial]: Destroy Called");
        }
        void OnGUI()
        {
            //The Addon's OnGUI() method is invoked by the game engine.  Must call all other custom OnGUI methods from here.
            Logger.OnGUI();
        }
        public void onSceneSwitchRequest(GameEvents.FromToAction<GameScenes,GameScenes> action){
            Logger.Info("Scene Changed from " + action.from + " to " + action.to);
        }


    }
}
