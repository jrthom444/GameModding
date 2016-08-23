using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPAddonBasics
{
    public enum DebugModes {ERROR = 1, INFO = 2, DEBUG = 3, TRACE = 4};
    
    public class DebugWindow : ScrollableWindow
    {
        private StringBuilder _sbuilder = new StringBuilder();
        public DebugModes DebugMode { get; set; }
        
        public DebugWindow(WindowDimensions dimension) : base(dimension) { }

        public void Info(string value)
        {
            if (DebugModes.INFO <= DebugMode)
            {
                _sbuilder.Append("[" + GetCurrentTime() + "][I] " + value + "\r\n");
            }
        }

        public void Debug(string value)
        {
            if (DebugModes.DEBUG <= DebugMode)
            {
                _sbuilder.Append("[" + GetCurrentTime() + "][D] " + value + "\r\n");
            }
        }

        public void Trace(string value)
        {
            if (DebugModes.TRACE <= DebugMode)
            {
                _sbuilder.Append("[" + GetCurrentTime() + "][T] " + value + "\r\n");
            }
        }

        public void Error(string value)
        {
            _sbuilder.Append("[" + GetCurrentTime() + "][ERROR] " + value + "\r\n");
        }

        public void ClearLogs()
        {
            _sbuilder.Length = 0;
        }

        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("hh:mm:ss");
        }

        public override void ScrollableContent(){
            GUILayout.Label(_sbuilder.ToString());
        }
    }
}
