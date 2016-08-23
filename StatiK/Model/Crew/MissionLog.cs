using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatiK.Model.Crew
{
    public enum MissionType {CAREER, FLIGHT};
    public class MissionLog
    {
        public MissionType Type; 
        public int Id;
        public List<string> Events;
    }
}
