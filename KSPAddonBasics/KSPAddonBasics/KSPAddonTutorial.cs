using KSP.UI.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.IO;

namespace KSPAddonBasics
{
    [KSPAddon(KSPAddon.Startup.Instantly, false)]
    class KSPAddonTutorial : MonoBehaviour
    {
        private DebugWindow Logger;
        private static ApplicationLauncherButton appButton = null;
        private static string AddonRootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        public KSPAddonTutorial()
        {
            //Use KSP's built-in logger to show initialization.
            Debug.Log("[KSPAddonTutorial]:Addon Initialized");
        }
        void Awake()
        {
            //Create a Unity IMGUI.GUILayout window designed to show debug statements.
            Logger = new DebugWindow(new WindowDimensions { Top = 100, Left = 100, Height = 300, Width = 800 });
            Logger.DebugMode = DebugModes.INFO;
            GameEvents.onGameSceneSwitchRequested.Add(onSceneSwitchRequest);
            DontDestroyOnLoad(this);
        }
        void Start()
        {
            Logger.Debug("Start Called: Window Initialized: " + Logger.WindowName);
            Logger.Debug("RootDir=" + AddonRootDir);
            GameEvents.onGUIApplicationLauncherReady.Add(new EventVoid.OnEvent(InitializeAppLauncherButton));
            GameEvents.onGameStateLoad.Add(new EventData<ConfigNode>.OnEvent(onGameStateLoad));
            GameEvents.onGameStateSave.Add(new EventData<ConfigNode>.OnEvent(onGameStateSave));
            
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

        void InitializeAppLauncherButton()
        {
            Texture2D debugButton = new Texture2D(32, 32, TextureFormat.ARGB32, false);
            try
            {
                debugButton.LoadImage(System.IO.File.ReadAllBytes(AddonRootDir + "\\Debug_small.png"));
            }
            catch (Exception e) 
            { 
                Logger.Error("icon file not found");
                Logger.Error(e.Message);
            }

            ApplicationLauncher.AppScenes buttonVisibleOnScenes = ApplicationLauncher.AppScenes.SPACECENTER;
            if (ApplicationLauncher.Ready && appButton == null)
            {
                appButton = ApplicationLauncher.Instance.AddModApplication(Logger.Show, Logger.Hide, null, null, null, null, buttonVisibleOnScenes, (Texture)debugButton);
            }

        }

        public void onGameStateSave(ConfigNode node)
        {
            Logger.Trace("OnGameStateSave:" + node.name);
        }
        public void onGameStateLoad(ConfigNode node)
        {
            try
            {
                ConfigNode rosterNode = new ConfigNode();
                Logger.Trace("OnGameStateLoad: " + node.name);
                ConfigNode[] nodes = node.GetNodes();
                if (node.TryGetNode("ROSTER", ref rosterNode))
                {
                    foreach (ConfigNode crewNode in rosterNode.GetNodes())
                    {
                        CrewMember crew = new CrewMember();
                        ConfigNode.LoadObjectFromConfig(crew, crewNode);
                        Logger.Info(crew.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e.Message);
            }
            

        }

        public void onSceneSwitchRequest(GameEvents.FromToAction<GameScenes,GameScenes> action){
            Logger.Debug("Scene Changed from " + action.from + " to " + action.to);
            Logger.Debug("LoadedSceneIsGame: " + HighLogic.LoadedSceneIsGame);
            if(action.to.Equals(GameScenes.SPACECENTER))
            {
                Logger.Debug("Save Folder: " + HighLogic.SaveFolder);
            }
        }




    }
}
