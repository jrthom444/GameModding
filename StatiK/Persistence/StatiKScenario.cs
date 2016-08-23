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
        private ILogger log; 
        public StatiKScenario()
        {
            log = StatikLogManager.Instance.GetLogger(this.GetType().Name);
        }

        public override void OnLoad(ConfigNode node)
        {
            log.Trace("OnLoad Called");
            base.OnLoad(node);
            if (node != null)
            {
                Statistics stats = StatiKCore.Instance.StatisticsService.StatiKData;
                if (ConfigNode.LoadObjectFromConfig(stats, node.GetNode("Statistics")))
                {
                    log.Info(stats.ToString());
                }
                else
                {
                    log.Error("Failed to load statistics object");
                }
            }
            else
            {
                log.Info("StatiKScenario");
            }
        }

        public override void OnSave(ConfigNode node)
        {
            log.Trace("OnSave Called");
            base.OnSave(node);
            Statistics s = StatiKCore.Instance.StatisticsService.StatiKData;
            log.Info("Saving Statistics: " + s.ToString());
            ConfigNode newData = new ConfigNode(s.GetType().Name);
            newData = ConfigNode.CreateConfigFromObject(s, newData);
            node.AddNode(newData);
        }
    }
}
