using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPAddonBasics
{
    public abstract class ScrollableWindow
    {
        public int _windowId;
        public Rect _container;
        public WindowDimensions _dimensions;
        private Vector2 scrollPosition = Vector2.zero;
        public string WindowName;
        private bool _shouldReDraw = true;
        

        public ScrollableWindow(WindowDimensions dimensions)
        {
            _dimensions = dimensions;
            _windowId = this.GetType().Name.GetHashCode();
            WindowName = this.GetType().Name + "(" + _windowId + ")"; 
            _container = dimensions.GetRect();
        }

        public void OnGUI()
        {
            if (_shouldReDraw)
            {
                _container = GUILayout.Window(_windowId, _container, DrawWindow, "KSPTutorialAddon");
            }
        }
        
        void DrawWindow(int windowId)
        {
            
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(_dimensions.Width), GUILayout.Height(_dimensions.Height));
            ScrollableContent();
            GUILayout.EndScrollView();
            GUI.DragWindow();
        }

        public void Show()
        {
            _shouldReDraw = true;
        }

        public void Hide()
        {
            _shouldReDraw = false;
        }

        public abstract void ScrollableContent();

    }
}
