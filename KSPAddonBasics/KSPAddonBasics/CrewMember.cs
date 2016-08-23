using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP.IO;


namespace KSPAddonBasics
{
    public class CrewMember
    {
        [Persistent]
        public string name;
        [Persistent]
        public string gender;
        [Persistent]
        public string state;

        public override string ToString()
        {
            string output = "[CrewMember:[name:{0},gender{1},state{2}]]";
            output = string.Format(output, name, gender, state);
            return output;
        }
    }
}
