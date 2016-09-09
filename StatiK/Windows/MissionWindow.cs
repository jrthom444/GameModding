using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP.UI.Screens;
using StatiK.Utils;
using UnityEngine;
using StatiK.Model;

namespace StatiK.Windows
{
    public class MissionWindowEventArgs : EventArgs
    {
        public Mission Mission { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
    }
    
    public class MissionWindow : BaseWindow
    {
        private ILogger log = StatikLogManager.Instance.GetLogger(typeof(MissionWindow).Name);
        private ApplicationLauncherButton _appButton;
        private string _missionName = "";
        private string _missionDescription = "";
        private Mission _mission;

        public MissionWindow(WindowSettings settings) : base(settings) 
        {
            SaveButtonClick += InternalSaveHandler;
        }

        public event EventHandler<MissionWindowEventArgs> SaveButtonClick;
        
        public static WindowSettings GetDefaultSettings()
        {
            return new WindowSettings
            {
                Name = "MissionWindow",
                Title = "Mission",
                Dimensions = new GUIDimensions { Top = 100, Left = 800, Height = 200, Width = 400 }
            };
        }

        private void InitializeData()
        {
            _mission = null;
            string currentVesselId = FlightGlobals.ActiveVessel.id.ToString();
            StatiKData data = StatiKCore.Instance.StatisticsService.StatiKData;
            _mission = data.Missions.Where<Mission>(m => m.VesselId == currentVesselId).First();
        }

        public override void ScrollableContent()
        {
            GUI.FocusWindow(this._settings.Id);
            GUILayout.Label("Mission Name");
            _missionName = GUILayout.TextField(_missionName);
            GUILayout.Label("Mission Description");
            _missionDescription = GUILayout.TextArea(_missionDescription);
            if(GUILayout.Button("Save"))
            {
                if(SaveButtonClick != null)
                {
                    SaveButtonClick(this, new MissionWindowEventArgs {Mission = _mission, Name = _missionName, Desc = _missionDescription });
                }
            }
        }

        public void InternalSaveHandler(object sender, MissionWindowEventArgs args)
        {
            this.Hide(CloseAction.WorkflowEvent);
        }

        public override void InitializeAppLauncherButton()
        {
            ApplicationLauncher.AppScenes eligibleGameScenes = ApplicationLauncher.AppScenes.FLIGHT;
            if (_appButton == null)
            {
                _appButton = ApplicationLauncher.Instance.AddModApplication(this.Show, this.AppLaunchHide, null, null, null, null, eligibleGameScenes, ResourceUtil.Instance.StatiKIcon);
            }
        }

        public override void DestoryAppLauncherButton()
        {
            ApplicationLauncher.Instance.RemoveApplication(_appButton);
        }
    }
}
