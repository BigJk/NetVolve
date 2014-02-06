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

        public static WarriorLine GetWarriorLine(string line)
        {
            string[] parts = line.Replace(" ", " ")
                .Split(new[] {' ', ',', '.'}, StringSplitOptions.RemoveEmptyEntries);
            return new WarriorLine(IdentifyInstructor(parts[0]), IdentifyModifier(parts[1]),
                IdenfifyAddressingMode(parts[2]), IdenfifyAddressingMode(parts[4]), short.Parse(parts[3]),
                short.Parse(parts[5]));
        }

        public static WarriorEndLine GetWarriorEndLine(string line)
        {
            string[] parts = line.Replace(" ", " ").Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return new WarriorEndLine(short.Parse(parts[1]));
        }

        public static bool IsEndLine(string line)
        {
            return line.ToLower().StartsWith("end");
        }

        public static Instructors IdentifyInstructor(string s)
        {
            return (Instructors)Enum.Parse(typeof (Instructors), s.ToLower());
        }

        public static Modifiers IdentifyModifier(string s)
        {
            return (Modifiers)Enum.Parse(typeof(Modifiers), s.ToLower());
        }

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
