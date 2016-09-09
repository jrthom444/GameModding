using StatiK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StatiK.Windows
{
    public class MissionDetailsWindow : BaseWindow
    {
        private Mission _mission;
        public MissionDetailsWindow(WindowSettings settings, Mission mission) : base(settings) 
        {
            _mission = mission;
        }

        public override void ScrollableContent()
        {
            GUILayout.Label(_mission.Name);
        }

        public override void InitializeAppLauncherButton()
        {
            throw new NotImplementedException();
        }

        public override void DestoryAppLauncherButton()
        {
            throw new NotImplementedException();
        }

    }
}
