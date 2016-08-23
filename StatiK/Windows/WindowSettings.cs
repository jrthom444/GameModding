using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatiK.Windows
{
    public class WindowSettings
    {
        private int _id;
        private string _name;

        public int Id { get { return _id; } }
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                _id = _name.GetHashCode();
            } 
        }
        public string Title { get; set; }
        public GUIDimensions Dimensions { get; set; }
    }
}
