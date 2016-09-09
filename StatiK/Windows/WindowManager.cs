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

        private static WindowManager _instance;
        public static WindowManager Instance 
        {
            get 
            {
                if(_instance == null)
                {
                    _instance = new WindowManager();
                }
                return _instance;
            } 
        }
        
        private List<BaseWindow> _windows = new List<BaseWindow>();
        private WindowManager() 
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

        public void DestroyWindows()
        {
            log.Trace("Destroying AppLauncher Buttons");
            _windows.ForEach(w => w.DestoryAppLauncherButton());
        }

        public bool IsWindowRegistered(int id)
        {
            return _windows.Any<BaseWindow>(w => w.Id == id);
        }

        public void UnregisterWindow(BaseWindow window)
        {
            _windows.Remove(window);
        }
    }
}
