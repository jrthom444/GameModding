using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatiK.Model;

namespace StatiK
{
    public class StatisticsService
    {
        public KSPData StartingData { get; set; }
        private Statistics _statikData;

        public Statistics StatiKData { get { return _statikData; } }
        
        public StatisticsService()
        {
            _statikData = new Statistics();
            StartingData = new KSPData();
        }

   
        public void DebitFunds(double NewTotalFunds)
        {
            _statikData.CashSpent += (StartingData.Funds.funds - NewTotalFunds);
        }

        public void CreditFunds(double NewTotalFunds)
        {
            _statikData.CashEarned += (NewTotalFunds - StartingData.Funds.funds);
        }
    }
}
