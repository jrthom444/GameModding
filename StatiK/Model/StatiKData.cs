using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatiK.Model
{
    public class StatiKData
    {
        public static List<TransactionReasons> TransactionDebits = new List<TransactionReasons>
        {
            TransactionReasons.CrewRecruited,
            TransactionReasons.ContractPenalty,
            TransactionReasons.RnDPartPurchase,
            TransactionReasons.StructureConstruction,
            TransactionReasons.VesselRollout
        };

        public StatiKData()
        {
            CashEarned = 0.0d;
            CashSpent = 0.0d;
            Missions = new List<Mission>();
        }

        [Persistent]
        public double CashEarned;
        [Persistent]
        public double CashSpent;
        [Persistent]
        public List<Mission> Missions;

        public override string ToString()
        {
            return String.Format("[Stastics]\r\n\tCashEarned:{0},\r\n\tCashSpent{1}", CashEarned, CashSpent);
        }
    }
}
