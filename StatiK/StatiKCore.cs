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
        private StatiKDataService _statisticsService;
        public StatiKDataService StatisticsService { get { return _statisticsService; } }

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
            _statisticsService = new StatiKDataService();
            _windowManager = WindowManager.Instance;
            _windowManager.RegisterWindow(new DebugWindow(DebugWindow.GetDefaultSettings()));
            _windowManager.RegisterWindow(new SpaceCenterWindow(SpaceCenterWindow.GetDefaultSettings()));

            MissionWindow missionWindow = new MissionWindow(MissionWindow.GetDefaultSettings());
            missionWindow.SaveButtonClick += _statisticsService.AddMission;
            _windowManager.RegisterWindow(missionWindow);
        }
        
        public void DrawGUI()
        {
            _windowManager.DrawWindows();
        }

        public void onApplicationLauncherReady()
        {
            _windowManager.InitStatiKWindows();
        }

        public void onApplicationLauncherDestroy()
        {
            _windowManager.DestroyWindows();
        }

        public void onFundsChange(double value, TransactionReasons reason)
        {
            if (StatiKData.TransactionDebits.Any<TransactionReasons>(r => r == reason))
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
            Log.Trace("onVesselRecovered Called");
            SessionState.FlightRecovered();
            ProcessSessionState();
        }

        public void onVesselRollout(ShipConstruct shipConstruct)
        {
            Log.Trace("onVesselRollout Called");
            SessionState.FlightRolledOut();
            ProcessSessionState();
        }

        public void onVesselSwitch(Vessel from, Vessel to)
        {
            Log.Trace("onVesselSwitch Called");
            SessionState.SwitchedVessels();
            ProcessSessionState();
        }

        public void onSceneSwitchRequest(GameEvents.FromToAction<GameScenes, GameScenes> action)
        {
            Log.Trace("Scene Switch Called");
            SessionState.SceneChanged(action.from, action.to);
            ProcessSessionState();
        }

        public void RegisterWindow(BaseWindow window)
        {
            _windowManager.RegisterWindow(window);
        }

        private void ProcessSessionState()
        {
            switch (SessionState.CurrentPlayerAction)
            {
                case PlayerAction.START:
                    _statisticsService.StartingData = GameDataService.LoadKSPData();
                    Log.Info("Starting Data: " + _statisticsService.StartingData.ToString());
                    break;
                case PlayerAction.EDITOR_REVERT:
                    _statisticsService.RollbackFlight();
                    break;
                case PlayerAction.ROLLOUT_FLIGHT:
                case PlayerAction.FLYING:
                    _statisticsService.StartFlight();
                    break;
                case PlayerAction.END_FLYING:
                    _statisticsService.CommitFlight();
                    break;
            }
        }

        public void onGameStateLoad(ConfigNode node)
        {
            if (!_gameLoaded)
            {
                List<ProtoScenarioModule> modules = HighLogic.CurrentGame.scenarios;
                ProtoScenarioModule scenarioModule = modules.FirstOrDefault(s => s.moduleName == typeof(StatiKScenario).Name);
                if(scenarioModule == null)
                {
                    HighLogic.CurrentGame.AddProtoScenarioModule(typeof(StatiKScenario), GameScenes.SPACECENTER);
                }
                /*
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
                 */
            }
        }
    }
}
