using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;

namespace NetVolveLib.Evolver.Plugins
{
    public class ChangeModifiers : IEvolverPlugin
    {

        public string Name
        {
            get { return "ChangeModifiers"; }
        }

        public string Author
        {
            get { return "BigJK"; }
        }

        public string Description
        {
            get { return "Randomly changes Modifiers"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            for (int i = 0; i < Statics.MainRandom.Next(father.CodeLines.Count() / 2); i++)
            {
                father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())].Modifier = (Modifiers)(byte)Statics.MainRandom.Next(EnumHelper.GetEnumLen(typeof(Modifiers)));
            }
            return father;
        }

    }
}