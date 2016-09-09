using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatiK.Model;
using UnityEngine;
using StatiK.Windows;
using StatiK.Utils;

namespace StatiK.Controllers
{
    public delegate void MissionDetailClick(Mission mission);

    public class MissionHistoryController
    {
        private ILogger Log = StatikLogManager.Instance.GetLogger(typeof(MissionHistoryController).Name);
        private int _index;
        private bool _canShowData;
        private Mission[] missions = new Mission[5];

        private WindowManager _windowManager;
        private List<Mission> _missionList;

        public MissionHistoryController()
        {
            _windowManager = WindowManager.Instance;
        }

        public void Initialize()
        {
            Log.Info("Initializing Mission History Controller");
            _index = 0;
            _missionList = StatiKCore.Instance.StatisticsService.StatiKData.Missions;
            _canShowData = _missionList.Any<Mission>();
            NextPage();
        }

        public void DrawMissionHistory()
        {
            if (_canShowData)
            {
                GUILayout.BeginVertical();

                GUILayout.BeginHorizontal();
                if (missions[0] != null) { DrawMission(missions[0], DrawDetails); }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (missions[1] != null) { DrawMission(missions[1], DrawDetails); }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (missions[2] != null) { DrawMission(missions[2], DrawDetails); }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (missions[3] != null) { DrawMission(missions[3], DrawDetails); }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (missions[4] != null) { DrawMission(missions[4], DrawDetails); }
                GUILayout.EndHorizontal();

                GUILayout.EndVertical();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Previous Page"))
                {
                    PreviousPage();
                }
                if (GUILayout.Button("Next Page"))
                {
                    NextPage();
                }
                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Label("No Active Missions! Get Flyin!");
            }
        }

        private void NextPage()
        {
            if (_index < _missionList.Count())
            {
                for (int i = 0; i < 5; i++)
                {
                    missions[i] = _missionList[_index] != null ? _missionList[_index] : null;
                    _index++;
                }

            }
        }

        private void PreviousPage()
        {
            _index = _index - 4;
            if (_index < 0) { _index = 0; }
            NextPage();
        }

        private void DrawMission(Mission mission, MissionDetailClick clickHandler)
        {
            GUILayout.Label(mission.Name);
            if(GUILayout.Button("Details"))
            {
                clickHandler(mission);
            }
        }

        private WindowSettings CreateMissionWindowSettings(Mission mission)
        {
            return new WindowSettings
            {
                Name = mission.Id,
                Title = mission.Name,
                Dimensions = new GUIDimensions { Top = 100, Left = 800, Height = 200, Width = 200 }
            };
        }

        private void DrawDetails(Mission mission)
        {
            Log.Debug("Drawing Details window");
            if(!_windowManager.IsWindowRegistered(mission.Name.GetHashCode()))
            {
                WindowSettings settings = CreateMissionWindowSettings(mission);
                MissionDetailsWindow window = new MissionDetailsWindow(settings, mission);
                _windowManager.RegisterWindow(window);
                window.Show();
            }
        }

    }
}
