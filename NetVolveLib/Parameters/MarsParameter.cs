using System;

namespace NetVolveLib.Parameters
{
    [Serializable]
    public class MarsParameter
    {
        public string Type { get; set; }
        public int Coresize { get; set; }
        public int MaxProcess { get; set; }
        public int Cycles { get; set; }
        public int Rounds { get; set; }
        public int MaxWarriorLen { get; set; }
    }
}
