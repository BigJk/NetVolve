using System.Collections.Generic;
using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Lines;

namespace NetVolveLib.Evolver.Plugins
{
    public class RemoveLine : IEvolverPlugin
    {

        public string Name
        {
            get { return "RemoveLine"; }
        }

        public string Author
        {
            get { return "BigJK"; }
        }

        public string Description
        {
            get { return "Mixes 2 warriors"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            if (father.CodeLines.Count() <= 2) return father;
            List<WarriorLine> newLines = new List<WarriorLine>(father.CodeLines);
            newLines.RemoveRange(Statics.MainRandom.Next(father.CodeLines.Count()), 1);
            father.CodeLines = newLines.ToArray();
            return father;
        }

    }
}