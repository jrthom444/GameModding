using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatiK.Model;
using StatiK.Utils;
using StatiK.Persistence;
using StatiK.Windows;

namespace StatiK
{
    public class StatiKCore
    {

        private ILogger Log = StatikLogManager.Instance.GetLogger(typeof(StatiKCore).Name);
        private bool _gameLoaded = false;
        private static StatiKCore _instance;
        private WindowManager _windowManager;
        public PlayerSessionState SessionState;
        private StatisticsService _statisticsService;
        public StatisticsService StatisticsService { get { return _statisticsService; } }

        private StatiKCore() { Log.Info("StatikCore Initialized!"); }
        public static StatiKCore Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new StatiKCore();
                }
                return _instance;
            }
        }

        public void Initialize()
        {
            SessionState = new PlayerSessionState();
            _statisticsService = new StatisticsService();
            _windowManager = new WindowManager();
            _windowManager.RegisterWindow(new DebugWindow(DebugWindow.GetDefaultSettings()));
            _windowManager.RegisterWindow(new SpaceCenterWindow(SpaceCenterWindow.GetDefaultSettings()));
        }
        
        public void DrawGUI()
        {
            _windowManager.DrawWindows();
        }

        public void onApplicationLauncherReady()
        {
            _windowManager.InitStatiKWindows();
        }

        public void onFundsChange(double value, TransactionReasons reason)
        {
            if (Statistics.TransactionDebits.Any<TransactionReasons>(r => r == reason))
            {
                _statisticsService.DebitFunds(value);
            }
            else
            {
                _statisticsService.CreditFunds(value);
            }
        }

        public void onVesselRecovered(ProtoVessel vessel, bool value)
        {
            SessionState.FlightRecovered();
        }

        public void onVesselRollout(ShipConstruct shipConstruct)
        {
            SessionState.FlightRolledOut();
        }

        public void onVesselSwitch(Vessel from, Vessel to)
        {
            SessionState.SwitchedVessels();
        }

        public void onSceneSwitchRequest(GameEvents.FromToAction<GameScenes, GameScenes> action)
        {
            SessionState.SceneChanged(action.from, action.to);
            if(SessionState.CurrentPlayerAction == PlayerAction.START)
            {
                _statisticsService.StartingData = GameDataService.LoadKSPData();
                Log.Info("Starting Data: " + _statisticsService.StartingData.ToString());
            }
        }

        public void onGameStateLoad(ConfigNode node)
        {
            if (!_gameLoaded)
            {
                try
                {
                    List<ProtoScenarioModule> modules = HighLogic.CurrentGame.scenarios;
                    ProtoScenarioModule scenarioModule = modules.FirstOrDefault(s => s.moduleName == typeof(StatiKScenario).Name);
                    if (scenarioModule == null)
                    {
                        HighLogic.CurrentGame.AddProtoScenarioModule(typeof(StatiKScenario), HighLogic.LoadedScene);
                    }
                    else
                    {
                        if (scenarioModule.targetScenes.Any(s => s != HighLogic.LoadedScene))
                        {
                            scenarioModule.targetScenes.Add(HighLogic.LoadedScene);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error(e.Message + " " + e.StackTrace);
                }
                _gameLoaded = true;
            }
        }
    }
}
