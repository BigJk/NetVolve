using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;

namespace NetVolveLib.Evolver.Plugins
{
    public class ChangeNumbers : IEvolverPlugin
    {

        public string Name
        {
            get { return "ChangeNumbers"; }
        }

        public string Author
        {
            get { return "BigJk"; }
        }

        public string Description
        {
            get { return "Randomly changes Numbers"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            for (int i = 0; i < Statics.MainRandom.Next(father.CodeLines.Count() / 2); i++)
            {
                if (Statics.MainRandom.NextDouble() >= 0.5)
                    father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())].Number1 =
                        (short)Statics.MainRandom.Next(parameter.MarsParameters.Coresize);
                else
                    father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())].Number2 =
                        (short)Statics.MainRandom.Next(parameter.MarsParameters.Coresize);
            }
            return father;
        }

    }
}