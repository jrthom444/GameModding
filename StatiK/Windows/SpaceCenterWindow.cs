using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;
using StatiK.Model;
using StatiK.Controllers;
using StatiK.Utils;

namespace StatiK.Windows
{
    public class SpaceCenterWindow : BaseWindow
    {
        private ILogger Log = StatikLogManager.Instance.GetLogger(typeof(SpaceCenterWindow).Name);

        private int _selectedToolbarIndex = 0;
        private ApplicationLauncherButton _appButton;
        private MissionHistoryController _controller;

        public static WindowSettings GetDefaultSettings()
        {
            return new WindowSettings
            {
                Name = "SpaceCenterWindow",
                Title = "General Statistics",
                Dimensions = new GUIDimensions { Top = 100, Left = 100, Height = 400, Width = 800 }
            };
        }

        private string[] _windowTabs = new string[] { "Kerbals", "Missions", "Statistics" };
        
        public SpaceCenterWindow(WindowSettings settings) : base(settings) 
        {
            _controller = new MissionHistoryController();
        }
        
        public override void ScrollableContent()
        {
            _selectedToolbarIndex = GUILayout.Toolbar(_selectedToolbarIndex, _windowTabs);

            if (_selectedToolbarIndex == 1)
            {
                _controller.DrawMissionHistory();
            }
            else
            {
                GUILayout.Label("I'm the space center window!  Feature coming soon!");
            }
        }

        public override void OnShow()
        {
            Log.Debug("OnShow Called");
            base.OnShow();
            _controller.Initialize();
        }

        public override void InitializeAppLauncherButton()
        {
            ApplicationLauncher.AppScenes eligibleGameScenes = ApplicationLauncher.AppScenes.SPACECENTER;
            if (_appButton == null)
            {
                _appButton = ApplicationLauncher.Instance.AddModApplication(this.Show, this.Hide, null, null, null, null, eligibleGameScenes, ResourceUtil.Instance.StatiKIcon);
            }
        }

        public override void DestoryAppLauncherButton()
        {
            ApplicationLauncher.Instance.RemoveApplication(_appButton);
        }
    }
}
