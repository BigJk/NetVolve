using System.Collections.Generic;
using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Lines;

namespace NetVolveLib.Evolver.Plugins
{
    public class AddLine : IEvolverPluginExtended
    {

        public string Name
        {
            get { return "AddLine"; }
        }

        public string Author
        {
            get { return "BigJk"; }
        }

        public string Description
        {
            get { return "Adds a new line to the warrior"; }
        }

        public double Chance { get; set; }

        public bool Possible(Warrior father, Parameter parameter)
        {
            return father.CodeLines.Count() < parameter.MarsParameters.MaxWarriorLen - 1;
        }

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