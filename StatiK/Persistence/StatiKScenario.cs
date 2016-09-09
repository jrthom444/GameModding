using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP;
using StatiK.Windows;
using StatiK.Utils;
using StatiK.Model;

namespace StatiK.Persistence
{
    internal class StatiKScenario : ScenarioModule
    {
        private ILogger log = StatikLogManager.Instance.GetLogger(typeof(StatiKScenario).Name); 

        public override void OnLoad(ConfigNode node)
        {
            log.Trace("OnLoad Called: node=" + node.ToString());
            base.OnLoad(node);

            StatiKData stats = StatiKCore.Instance.StatisticsService.StatiKData;
            if (ConfigNode.LoadObjectFromConfig(stats, node.GetNode("StatiKScenario")))
            {
                log.Debug(stats.ToString());
            }
            else
            {
                log.Error("Failed to load statistics object");
            }

        }

        public override void OnSave(ConfigNode node)
        {
            log.Trace("OnSave Called");
            base.OnSave(node);
            StatiKData s = StatiKCore.Instance.StatisticsService.StatiKData;
            ConfigNode newData = new ConfigNode(s.GetType().Name);
            log.Debug("Saving Data: " + newData.ToString());
            newData = ConfigNode.CreateConfigFromObject(s, newData);
            node.AddNode(newData);
        }
    }
}
