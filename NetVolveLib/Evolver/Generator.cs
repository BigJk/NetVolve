using System.Linq;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;
using NetVolveLib.Redcode.Lines;

namespace NetVolveLib.Evolver
{
    public static class Generator
    {

        public static Instructors[] UsedInstructorses = new Instructors[0];

        /// <summary>
        /// Generates new random warrior
        /// </summary>
        /// <param name="type">Type name</param>
        /// <param name="name">Warrior name</param>
        /// <param name="author">Warrior author</param>
        /// <param name="lines">Amount of lines</param>
        /// <param name="endLine">Should there be an End line</param>
        /// <param name="coresize">Size of the core</param>
        /// <returns></returns>
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

        /// <summary>
        /// Generates one random line
        /// </summary>
        /// <param name="coresize">Size of the core</param>
        /// <returns></returns>
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
