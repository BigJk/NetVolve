using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;

namespace NetVolveLib.Evolver.Plugins
{
    public class ChangeAddressingModes : IEvolverPlugin
    {

        public string Name
        {
            get { return "ChangeAddressingModes"; }
        }

        public string Author
        {
            get { return "BigJk"; }
        }

        public string Description
        {
            get { return "Randomly changes AddressingModes"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            for (int i = 0; i < Statics.MainRandom.Next(father.CodeLines.Count() / 2); i++)
            {
                if (Statics.MainRandom.NextDouble() >= 0.5)
                    father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())].AddressingMode1 =
                        (AddressingModes)
                            (byte) Statics.MainRandom.Next(EnumHelper.GetEnumLen(typeof (AddressingModes)));
                else
                    father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())].AddressingMode2 =
                        (AddressingModes)
                            (byte) Statics.MainRandom.Next(EnumHelper.GetEnumLen(typeof (AddressingModes)));
            }
            return father;
        }

    }
}