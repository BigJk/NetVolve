using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Lines;

namespace NetVolveLib.Evolver.Plugins
{
    public class ChangeEnd : IEvolverPlugin
    {

        public string Name
        {
            get { return "ChangeEnd"; }
        }

        public string Author
        {
            get { return "BigJk"; }
        }

        public string Description
        {
            get { return "Randomly changes the Endline"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            father.EndLine = new WarriorEndLine((short)Statics.MainRandom.Next(father.CodeLines.Count()));
            return father;
        }

    }
}