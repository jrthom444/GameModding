using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatiK.Model
{
    public class Statistics
    {
        public static List<TransactionReasons> TransactionDebits = new List<TransactionReasons>
            {
                TransactionReasons.CrewRecruited,
                TransactionReasons.ContractPenalty,
                TransactionReasons.RnDPartPurchase,
                TransactionReasons.StructureConstruction,
                TransactionReasons.VesselRollout
            };

        public Statistics()
        {
            CashEarned = 0.0d;
            CashSpent = 0.0d;
        }

        [Persistent]
        public double CashEarned;
        [Persistent]
        public double CashSpent;

        public override string ToString()
        {
            return String.Format("[Stastics]\r\n\tCashEarned:{0},\r\n\tCashSpent{1}", CashEarned, CashSpent);
        }
    }
}
