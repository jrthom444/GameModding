using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatiK.Model;
using StatiK.Utils;
using StatiK.Windows;

namespace StatiK
{
    public class StatiKDataService
    {
        private ILogger log = StatikLogManager.Instance.GetLogger(typeof(StatiKDataService).Name); 
        public KSPData StartingData { get; set; }

        private StatiKData _statiKData;
        private StatiKData _commitData;

        public StatiKData StatiKData { get { return _statiKData; } }
        
        public StatiKDataService()
        {
            _statiKData = new StatiKData();
            StartingData = new KSPData();
        }

        public void StartFlight()
        {
            _commitData = new StatiKData();
            log.Debug("Flight Started");
        }

        public void CommitFlight()
        {
            _statiKData.CashEarned += _commitData.CashEarned;
            _statiKData.CashSpent += _commitData.CashSpent;
            _statiKData.Missions.AddRange(_commitData.Missions);
            log.Debug("Flight Commited");
        }

        public void RollbackFlight()
        {
            _commitData = null;
            log.Debug("Flight Rolled back");
        }

        public void DebitFunds(double NewTotalFunds)
        {
            if (_commitData != null)
            {
                _commitData.CashSpent += (StartingData.Funds.funds - NewTotalFunds);
            }
            else
            {
                log.Error("DebitFunds: No Flight started, cannot record data");
            }
        }

        public void CreditFunds(double NewTotalFunds)
        {
            if (_commitData != null)
            {
                _commitData.CashEarned += (NewTotalFunds - StartingData.Funds.funds);
            }
            else
            {
                log.Error("CreditFunds: No Flight started, cannot record data");
            }
        }

        public void AddMission(object sender, MissionWindowEventArgs args)
        {
            if(_commitData != null)
            {
                log.Debug("Adding Mission");
                Mission mission = new Mission
                {
                    Id = Guid.NewGuid().ToString(),
                    VesselId = FlightGlobals.ActiveVessel.id.ToString(),
                    Name = args.Name,
                    Description = args.Desc
                };

                _commitData.Missions.Add(mission);
            }
            else
            {
                log.Error("AddMission: No Flight started, cannot record data");
            }
        }
    }
}
