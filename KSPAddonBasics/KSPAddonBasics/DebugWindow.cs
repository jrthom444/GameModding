using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPAddonBasics
{
    public class DebugWindow : ScrollableWindow
    {
        private StringBuilder _sbuilder = new StringBuilder();
        
        public DebugWindow(WindowDimensions dimension) : base(dimension) { }

        public void Info(string value)
        {
            _sbuilder.Append("[" + WindowConstants.ADDON_NAME + "][INFO]" + value + "\r\n");
        }

        public void Debug(string value)
        {
            _sbuilder.Append("[" + WindowConstants.ADDON_NAME + "][DEBUG]" + value + "\r\n");
        }

        public void Error(string value)
        {
            _sbuilder.Append("[" + WindowConstants.ADDON_NAME + "][ERROR]" + value + "\r\n");
        }

        public void ClearLogs()
        {
            _sbuilder.Length = 0;
        }

        public override void ScrollableContent(){
            GUILayout.Label(_sbuilder.ToString());
        }
    }
}
