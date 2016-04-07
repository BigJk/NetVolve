using System.Linq;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;

namespace NetVolveLib.Evolver.Plugins
{
    public class ChangeInstructors : IEvolverPlugin
    {

        public string Name
        {
            get { return "ChangeInstructors"; }
        }

        public string Author
        {
            get { return "BigJk"; }
        }

        public string Description
        {
            get { return "Randomly changes Instructors"; }
        }

        public double Chance { get; set; }

        public Warrior Execute(Warrior father, Warrior mother, Parameter parameter)
        {
            for (int i = 0; i < Statics.MainRandom.Next(father.CodeLines.Count() / 2); i++)
            {
                father.CodeLines[Statics.MainRandom.Next(father.CodeLines.Count())].Instructor = (Instructors)(byte)Statics.MainRandom.Next(EnumHelper.GetEnumLen(typeof(Instructors)));
            }
            return father;
        }

    }
}
