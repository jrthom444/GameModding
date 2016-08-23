using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatiK.Model.Crew
{
    public class CrewMember
    {
        [Persistent]
        public string Name;
        [Persistent]
        public string Gender;
        [Persistent]
        public string Type;
        [Persistent]
        public string State;
        [Persistent]
        public string Trait;
 
        public List<MissionLog> logs;
    }
}
