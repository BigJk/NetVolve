using System.Linq;
using NetVolveLib.Redcode.Enums;
using NetVolveLib.Redcode.Lines;
using System;
using System.Collections.Generic;
using System.IO;

namespace NetVolveLib.Redcode
{
    public static class Parser
    {

        /// <summary>
        /// Creates a Warrior object from a Redcode file.
        /// </summary>
        /// <param name="path">Path to Redcode file</param>
        /// <returns>The resulting 'Warrior' object</returns>
        public static Warrior WarriorFromOriginal(string path)
        {
            string name = "", author = "", type = "";
            List<WarriorLine> warriorLines = new List<WarriorLine>();
            WarriorEndLine endLine = null;

            string[] lines = File.ReadAllLines(path);

            for (int i = 0; i < lines.Count(); i++)
            {
                lines[i] = lines[i].Trim();
            }

            foreach (string line in lines)
            {
                if (line.StartsWith(";name "))
                    name = line.Replace(";name ", "");

                else if (line.StartsWith(";author "))
                    author = line.Replace(";author ", "");

                else if (line.StartsWith(";redcode-"))
                    type = line.Replace(";redcode-", "");

                else if (IsEndLine(line))
                    endLine = GetWarriorEndLine(line);

                else if (line.Length >= 3 && !line.StartsWith(";"))
                    warriorLines.Add(GetWarriorLine(line));
            }

            return endLine != null
                ? new Warrior(type, name, author, warriorLines.ToArray(), endLine)
                : new Warrior(type, name, author, warriorLines.ToArray());
        }

        /// <summary>
        /// Converts a Redcode line into a 'WarriorLine' object
        /// </summary>
        /// <param name="line">The Redcode line</param>
        /// <returns></returns>
        public static WarriorLine GetWarriorLine(string line)
        {
            string[] parts = line.Replace(" ", " ")
                .Split(new[] {' ', ',', '.'}, StringSplitOptions.RemoveEmptyEntries);
            return new WarriorLine(IdentifyInstructor(parts[0]), IdentifyModifier(parts[1]),
                IdenfifyAddressingMode(parts[2]), IdenfifyAddressingMode(parts[4]), short.Parse(parts[3]),
                short.Parse(parts[5]));
        }

        /// <summary>
        /// Converts the 'END' line into a 'WarriorEndLine' object
        /// </summary>
        /// <param name="line">The Redcode line</param>
        /// <returns></returns>
        public static WarriorEndLine GetWarriorEndLine(string line)
        {
            string[] parts = line.Replace(" ", " ").Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return new WarriorEndLine(short.Parse(parts[1]));
        }

        /// <summary>
        /// Checks if a Redcode line is the 'END' line
        /// </summary>
        /// <param name="line">The Redcode line</param>
        /// <returns></returns>
        public static bool IsEndLine(string line)
        {
            return line.ToLower().StartsWith("end");
        }

        /// <summary>
        /// Converts a string that contains a instructor into the fitting 'Instructors' type
        /// </summary>
        /// <param name="s">The instructor as string</param>
        /// <returns></returns>
        public static Instructors IdentifyInstructor(string s)
        {
            return (Instructors)Enum.Parse(typeof (Instructors), s.ToUpper());
        }

        /// <summary>
        /// Converts a string that contains a modifier into the fitting 'Modifiers' type
        /// </summary>
        /// <param name="s">The modifier as string</param>
        /// <returns></returns>
        public static Modifiers IdentifyModifier(string s)
        {
            return (Modifiers)Enum.Parse(typeof(Modifiers), s.ToUpper());
        }

        /// <summary>
        /// Converts a string that contains the addressing mode into the fitting 'AddressingModes' type
        /// </summary>
        /// <param name="s">The addressing mode as string</param>
        /// <returns></returns>
        public static AddressingModes IdenfifyAddressingMode(string s)
        {
            switch (s)
            {
                case "#":
                    return AddressingModes.sharp;
                case "$":
                    return AddressingModes.dollar;
                case "@":
                    return AddressingModes.at;
                case "*":
                    return AddressingModes.star;
                case "<":
                    return AddressingModes.lowerThen;
                case ">":
                    return AddressingModes.higherThen;
                case "{":
                    return AddressingModes.open;
                case "}":
                    return AddressingModes.close;
            }
            return AddressingModes.at;
        }

    }
}
