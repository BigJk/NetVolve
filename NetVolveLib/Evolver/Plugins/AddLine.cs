using System.Collections.Generic;
using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Lines;

namespace NetVolveLib.Evolver.Plugins
{
    public class AddLine : IEvolverPlugin
    {

        public string Name
        {
            get { return "AddLine"; }
        }

        public string Author
        {
            get { return "BigJK"; }
        }

        public string Description
        {
            get { return "Adds a new line to the warrior"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            if (father.CodeLines.Count() >= parameter.MarsParameters.MaxWarriorLen) return father;
            List<WarriorLine> newLines = new List<WarriorLine>(father.CodeLines);
            newLines.Insert(Statics.MainRandom.Next(father.CodeLines.Count()), Generator.GenerateRandomLine(parameter.MarsParameters.Coresize));
            father.CodeLines = newLines.ToArray();
            return father;
        }

    }
}