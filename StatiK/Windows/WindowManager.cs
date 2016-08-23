using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP.UI.Screens;
using StatiK.Utils;

namespace StatiK.Windows
{
    public class WindowManager
    {
        private ILogger log;

        private List<BaseWindow> _windows = new List<BaseWindow>();
        public WindowManager() 
        {
            log = StatikLogManager.Instance.GetLogger(this.GetType().Name);
            log.Trace("Window Manager Created");
        }

        

        public void RegisterWindow(BaseWindow window)
        {
            _windows.Add(window);
        }

        public void DrawWindows()
        {
            _windows.ForEach(w => w.OnGUI());
        }

        public void InitStatiKWindows()
        {
            log.Trace("Initializing AppLauncher Buttons");
            _windows.ForEach(w => w.InitializeAppLauncherButton());
        }
    }
}
