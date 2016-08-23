using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatiK.Model;
using StatiK.Utils;

namespace StatiK.Persistence
{
    public static class GameDataService
    {
        private static ILogger log = StatikLogManager.Instance.GetLogger(typeof(GameDataService).Name);
        public static KSPData LoadKSPData()
        {
            //ConfigNode node = HighLogic.CurrentGame.config;
            KSPData data = new KSPData();
            data.Funds.funds = 122036.8d;
            //LoadFundsData(node, data);
            
            return data;
        }

        private static void LoadFundsData(ConfigNode node, KSPData data)
        {
            KSPFunds funds = new KSPFunds();
            if (ConfigNode.LoadObjectFromConfig(funds, node.GetNode("Funding")))
            {
                data.Funds = funds;
            }
            else
            {
                log.Error("Config Node Funds note found");
            }
        }
    }
}
