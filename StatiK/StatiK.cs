using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using StatiK.Windows;
using StatiK.Utils;
using StatiK.Persistence;

namespace StatiK
{
    [KSPAddon(KSPAddon.Startup.MainMenu, false)]
    public class StatiK : MonoBehaviour
    {
        private ILogger Log = StatikLogManager.Instance.GetLogger(typeof(StatiK).Name);
        void Awake()
        {
            try
            {
                StatikLogManager.Instance.LogLevel = LogLevels.TRACE;
                StatiKCore.Instance.Initialize();

                DontDestroyOnLoad(this);

                Log.Info("StatiK Addon Initialized");
            }
            catch (Exception e)
            {
                Debug.Log("Error Occured: " + e.Message);
            }
        }


        void Start()
        {
            GameEvents.onGUIApplicationLauncherReady.Add(new EventVoid.OnEvent(StatiKCore.Instance.onApplicationLauncherReady));
            GameEvents.onGameStateLoad.Add(new EventData<ConfigNode>.OnEvent(StatiKCore.Instance.onGameStateLoad));
            GameEvents.OnVesselRollout.Add(new EventData<ShipConstruct>.OnEvent(StatiKCore.Instance.onVesselRollout));
            GameEvents.OnFundsChanged.Add(new EventData<double, TransactionReasons>.OnEvent(StatiKCore.Instance.onFundsChange));
            GameEvents.onGameSceneSwitchRequested.Add(StatiKCore.Instance.onSceneSwitchRequest);
            GameEvents.onVesselRecovered.Add(new EventData<ProtoVessel, bool>.OnEvent(StatiKCore.Instance.onVesselRecovered));
            GameEvents.onVesselSwitching.Add(new EventData<Vessel, Vessel>.OnEvent(StatiKCore.Instance.onVesselSwitch));
        }

        void OnDestroy()
        {

        }

        void OnGUI()
        {
            StatiKCore.Instance.DrawGUI();
        }

    }        
}
