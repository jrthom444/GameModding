using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using KSP.UI.Screens;
using StatiK.Utils;

namespace StatiK.Windows
{
    public enum DebugModes { ERROR = 1, INFO = 2, DEBUG = 3, TRACE = 4 };

    public class DebugWindow : BaseWindow
    {
        public static WindowSettings GetDefaultSettings()
        {
            return new WindowSettings
            {
                Name = "DebugWindow",
                Title = "StatiK Logs",
                Dimensions = new GUIDimensions { Top = 0, Left = 0, Height = 400, Width = 800 }
            };
        }

        private ApplicationLauncherButton _appButton;

        public DebugWindow(WindowSettings settings) : base(settings) {  }

        public void ClearLogs()
        {
            StatikLogManager.Instance.Clear();
        }

        public override void ScrollableContent()
        {
            GUILayout.Label(StatikLogManager.Instance.GetLogEntriesAsString());
        }

        public override void InitializeAppLauncherButton()
        {
            ApplicationLauncher.AppScenes eligibleGameScenes = ApplicationLauncher.AppScenes.SPACECENTER | ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.FLIGHT;
            if (_appButton == null)
            {
                _appButton = ApplicationLauncher.Instance.AddModApplication(this.Show, this.AppLaunchHide, null, null, null, null, eligibleGameScenes, ResourceUtil.Instance.DebugButtonTexture);
            }
        }

        public override void DestoryAppLauncherButton()
        {
            ApplicationLauncher.Instance.RemoveApplication(_appButton);
        }
    }
}

