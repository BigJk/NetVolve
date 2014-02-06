using System;
using NetVolveLib.Redcode.Enums;

namespace NetVolveLib.Parameters
{
    [Serializable]
    public class EvolverParameter
    {
        
        public double EvolutionChance { get; set; }
        public string UsedInstructors { get; set; }
        public Instructors[] Instructors { get; set; }
        public PluginChance[] Plugins { get; set; }

    }

    [Serializable]
    public class PluginChance
    {
        public string Name { get; set; }
        public double Chance { get; set; }
    }

}
