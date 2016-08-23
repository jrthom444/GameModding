using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatiK.Model
{
    public class KSPData
    {
        public KSPData()
        {
            Funds = new KSPFunds();
        }

        public KSPFunds Funds { get; set; }
        public override string ToString()
        {
            return string.Format("KSPData[\r\n\t{0}\r\n]", Funds.ToString());
        }
    }

    public class KSPFunds
    {
        [Persistent]
        public double funds;
        public override string ToString()
        {
            return string.Format("KSPFunds[funds:{0}]", funds);
        }
    }

}
