using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJson;

namespace StatiK.Model
{
    public class Mission
    {
        public static int STATUS_ACTIVE = 0;
        public static int STATUS_COMPLETE = 1;

        [Persistent]
        public string Id;
        [Persistent]
        public string VesselId;
        [Persistent]
        public string Name;
        [Persistent]
        public string Description;
        [Persistent]
        public int Status;
        [Persistent]
        public double DeltaFunds;
        [Persistent]
        public double DeltaReputation;
        [Persistent]
        public double DeltaScience;
    }
}
