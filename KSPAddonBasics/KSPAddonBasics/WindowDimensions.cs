using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace KSPAddonBasics
{
    public class WindowDimensions
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rect GetRect()
        {
            return new Rect(Top, Left, Width, Height);
        }
    }
}
