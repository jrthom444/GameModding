using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StatiK.Windows
{
    public abstract class BaseWindow : IEquatable<BaseWindow>
    {
        public Rect _container;
        public WindowSettings _settings;
        private Vector2 scrollPosition = Vector2.zero;
        private bool _shouldReDraw = false;

        public int Id 
        {
            get
            {
                return _settings.Id;
            }
        }

        public BaseWindow(WindowSettings settings)
        {
            _settings = settings;
            _container = _settings.Dimensions.GetRect();
        }

        public void OnGUI()
        {
            if (_shouldReDraw)
            {
                _container = GUILayout.Window(_settings.Id, _container, DrawWindow, _settings.Title);
            }
        }
        
        void DrawWindow(int windowId)
        {
            
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(_settings.Dimensions.Width), GUILayout.Height(_settings.Dimensions.Height));
            ScrollableContent();
            GUILayout.EndScrollView();
            GUI.DragWindow();
        }

        public void Show()
        {
            _shouldReDraw = true;
            OnShow();
        }

        public void Hide()
        {
            _shouldReDraw = false;
            OnHide();
        }

        public bool Equals(BaseWindow window)
        {
            if (window == null)
            {
                return false;
            }
            return this.Id == window.Id;
        }

        public virtual void OnShow() {}

        public virtual void OnHide() {}

        public abstract void InitializeAppLauncherButton();
        public abstract void DestoryAppLauncherButton();
        public abstract void ScrollableContent();
    }
}
