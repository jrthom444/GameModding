using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StatiK
{
    public class ResourceUtil
    {
        public const int TEXTURE_SIZE = 32;
        private static ResourceUtil _instance;
        private string _addonRootDir;
        
        private Texture _debugBtnTexture;
        private Texture _statiKIcon;
        
        private string _debugTexturePath;
        private string _statiKIconPath;
        

        private ResourceUtil() 
        {
            _addonRootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            _debugTexturePath = string.Format("{0}\\{1}", _addonRootDir, "Debug_small.png");
            _statiKIconPath = string.Format("{0}\\{1}", _addonRootDir, "statiK.png");   
        }
        public static ResourceUtil Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ResourceUtil();
                }
                return _instance;
            }
        }

        public Texture DebugButtonTexture
        {
            get
            {
                if(_debugBtnTexture == null)
                {
                    _debugBtnTexture = LoadTexture(_debugTexturePath);
                }
                return _debugBtnTexture;
            }
        }

        public Texture StatiKIcon
        {
            get
            {
                if (_statiKIcon == null)
                {
                    _statiKIcon = LoadTexture(_statiKIconPath);
                }
                return _statiKIcon;
            }
        }

        private Texture LoadTexture(string path)
        {
            Texture2D text = new Texture2D(TEXTURE_SIZE, TEXTURE_SIZE, TextureFormat.ARGB32, false);
            text.LoadImage(System.IO.File.ReadAllBytes(path));
            return (Texture)text;
        }
    }
}
