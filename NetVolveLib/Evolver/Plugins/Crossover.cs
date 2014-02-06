using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Lines;
using NetVolveLib.Utility;

namespace NetVolveLib.Evolver.Plugins
{
    public class Crossover : IEvolverPlugin
    {

        public string Name
        {
            get { return "Crossover"; }
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
            for (int i = 0; i < Statics.MainRandom.Next(mother.CodeLines.Count() / 2); i++)
            {
                father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())] =
                    GenericCopier<WarriorLine>.DeepCopy(
                        mother.CodeLines[Statics.MainRandom.Next(mother.CodeLines.Count())]);
            }
            return father;
        }

    }
}