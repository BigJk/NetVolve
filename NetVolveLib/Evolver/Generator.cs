using System.Linq;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;
using NetVolveLib.Redcode.Lines;

namespace NetVolveLib.Evolver
{
    public static class Generator
    {

        public static Instructors[] UsedInstructorses = new Instructors[0];

        public static Warrior GenerateRandomWarrior(string type, string name, string author, int lines, bool endLine, int coresize)
        {
            WarriorLine[] warriorLines = new WarriorLine[lines];
            for (int i = 0; i < warriorLines.Count(); i++)
            {
                warriorLines[i] = GenerateRandomLine(coresize);
            }
            return endLine
                ? new Warrior(type, name, author, warriorLines, new WarriorEndLine((short) Statics.MainRandom.Next(lines)))
                : new Warrior(type, name, author, warriorLines);
        }

        public static WarriorLine GenerateRandomLine(int coresize)
        {
            return new WarriorLine(
                        UsedInstructorses[Statics.MainRandom.Next(UsedInstructorses.Length)],
                        (Modifiers)(byte)Statics.MainRandom.Next(EnumHelper.GetEnumLen(typeof(Modifiers))),
                        (AddressingModes)(byte)Statics.MainRandom.Next(EnumHelper.GetEnumLen(typeof(AddressingModes))),
                        (AddressingModes)(byte)Statics.MainRandom.Next(EnumHelper.GetEnumLen(typeof(AddressingModes))),
                        (short)Statics.MainRandom.Next(coresize), (short)Statics.MainRandom.Next(coresize));
        }

    }
}
