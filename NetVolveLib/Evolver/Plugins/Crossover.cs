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
            get { return "BigJk"; }
        }

        public string Description
        {
            get { return "Mixes 2 warriors"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            switch (Statics.MainRandom.Next(5))
            {
                case 0:
                    return Random(father, mother);
                case 1:
                    return UpperLower(father, mother);
                case 2:
                    return UpperUpper(father, mother);
                case 3:
                    return LowerLower(father, mother);
                case 4:
                    return LowerUpper(father, mother);
            }

            return Random(father, mother);
        }

        private Warrior Random(Warrior father, Warrior mother)
        {
            for (int i = 0; i < Statics.MainRandom.Next(father.CodeLines.Count() / 2); i++)
            {
                father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())] = mother.CodeLines[Statics.MainRandom.Next(mother.CodeLines.Count())].DeepCopy();
            }
            return father;
        }

        private Warrior UpperUpper(Warrior father, Warrior mother)
        {
            for (int i = 0; i < father.CodeLines.Count() / 2; i++)
            {
                if (i >= father.CodeLines.Length || i >= mother.CodeLines.Length)
                    break;
                father.CodeLines[i] = mother.CodeLines[i].DeepCopy();
            }
            return father;
        }

        private Warrior LowerLower(Warrior father, Warrior mother)
        {
            for (int i = 0; i < father.CodeLines.Count() / 2; i++)
            {
                if (father.CodeLines.Length - 1 - i < 0 || mother.CodeLines.Length - 1 - i < 0)
                    break;
                father.CodeLines[father.CodeLines.Length - 1 - i] = mother.CodeLines[mother.CodeLines.Length - 1 - i].DeepCopy();
            }
            return father;
        }

        private Warrior UpperLower(Warrior father, Warrior mother)
        {
            for (int i = 0; i < father.CodeLines.Count() / 2; i++)
            {
                if (i >= father.CodeLines.Length || mother.CodeLines.Length - 1 - i < 0)
                    break;
                father.CodeLines[i] = mother.CodeLines[mother.CodeLines.Length - 1 - i].DeepCopy();
            }
            return father;
        }

        private Warrior LowerUpper(Warrior father, Warrior mother)
        {
            for (int i = 0; i < father.CodeLines.Count() / 2; i++)
            {
                if (father.CodeLines.Length - 1 - i < 0 || i >= mother.CodeLines.Length)
                    break;
                father.CodeLines[father.CodeLines.Length - 1 - i] = mother.CodeLines[i].DeepCopy();
            }
            return father;
        }

    }
}