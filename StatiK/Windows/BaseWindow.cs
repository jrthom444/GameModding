using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StatiK.Windows
{
    
    public enum CloseAction {CloseButtonClick, AppLauncherClick, WorkflowEvent};
    public abstract class BaseWindow : IEquatable<BaseWindow>
    {
        public Rect _container;
        public WindowSettings _settings;
        private Vector2 scrollPosition = Vector2.zero;
        private bool _shouldReDraw = false;
        private Rect _closeButtonDim;

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
            _closeButtonDim  = (new GUIDimensions { Left = 3, Top = _settings.Dimensions.Width - 6, Height = 13, Width = 13 }).GetRect();
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

            if (GUI.Button(_closeButtonDim, ""))
            {
                Hide(CloseAction.CloseButtonClick);
            }
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

        public void AppLaunchHide()
        {
            Hide(CloseAction.AppLauncherClick);
        }

        public void Hide(CloseAction action)
        {
            _shouldReDraw = false;
            OnHide(action);
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

        public virtual void OnHide(CloseAction action) {}

        public abstract void InitializeAppLauncherButton();
        public abstract void DestoryAppLauncherButton();
        public abstract void ScrollableContent();
    }
}
