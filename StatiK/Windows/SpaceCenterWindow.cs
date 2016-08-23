using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;

namespace StatiK.Windows
{
    public class SpaceCenterWindow : BaseWindow
    {
        private int _selectedToolbarIndex = 0;
        private ApplicationLauncherButton appButton;

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
        
        public SpaceCenterWindow(WindowSettings settings) : base(settings) { }
        
        public override void ScrollableContent()
        {
            _selectedToolbarIndex = GUILayout.Toolbar(_selectedToolbarIndex, _windowTabs);
            GUILayout.Label("I'm the space center window!");
        }
        public override void InitializeAppLauncherButton()
        {
            ApplicationLauncher.AppScenes eligibleGameScenes = ApplicationLauncher.AppScenes.SPACECENTER;
            if (appButton == null)
            {
                appButton = ApplicationLauncher.Instance.AddModApplication(this.Show, this.Hide, null, null, null, null, eligibleGameScenes, ResourceUtil.Instance.StatiKIcon);
            }
        }
    }
}
