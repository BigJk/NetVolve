using System;

namespace NetVolveLib.Parameters
{
    [Serializable]
    public class GridParameter
    {
        public int Size { get; set; }
        public double PresetChance { get; set; }
        public int SleeperRate { get; set; }
        public int SleeperAmount { get; set; }
        public int ReplaceRate { get; set; }
        public int ReplaceAmount { get; set; }
        public double ReplacePresetChance { get; set; }
        public double ReplaceSleeperChance { get; set; }
    }
}
