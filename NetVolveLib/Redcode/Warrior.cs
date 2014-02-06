using NetVolveLib.Redcode.Lines;
using NetVolveLib.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NetVolveLib.Redcode
{
    [Serializable]
    public class Warrior : IEquatable<Warrior>
    {

        public string Type { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public WarriorLine[] CodeLines { get; set; }
        public WarriorEndLine EndLine { get; set; }

        public bool HasEndLine
        {
            get { return EndLine != null; }
        }

        public Warrior(string type, string name, string author, WarriorLine[] codeLines)
        {
            Type = type;
            Name = name;
            Author = author;
            CodeLines = codeLines;
        }

        public Warrior(string type, string name, string author, WarriorLine[] codeLines, WarriorEndLine endLine)
        {
            Type = type;
            Name = name;
            Author = author;
            CodeLines = codeLines;
            EndLine = endLine;
        }

        public void Save(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                using (BinaryWriter binWriter = new BinaryWriter(fileStream))
                {
                    binWriter.Write(Type);
                    binWriter.Write(Name);
                    binWriter.Write(Author);
                    binWriter.Write(HasEndLine);
                    binWriter.Write((short)CodeLines.Count());
                    foreach (WarriorLine line in CodeLines)
                    {
                        line.WriteToStream(binWriter);
                    }
                    if(HasEndLine) EndLine.WriteToStream(binWriter);
                }
            }
        }

        public static Warrior Load(string path)
        {
            string type, name, author;
            bool hasEndLine;

            List<WarriorLine> warriorLines = new List<WarriorLine>();
            WarriorEndLine warriorEndLine = null;

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (BinaryReader binReader = new BinaryReader(fileStream))
                {
                    type = binReader.ReadString();
                    name = binReader.ReadString();
                    author = binReader.ReadString();
                    hasEndLine = binReader.ReadBoolean();

                    short codeLines = binReader.ReadInt16();
                    for (int i = 0; i < codeLines; i++)
                    {
                        warriorLines.Add(WarriorLine.ReadFromStream(binReader));
                    }

                    if (hasEndLine)
                        warriorEndLine = WarriorEndLine.ReadFromStream(binReader);
                }
            }

            return hasEndLine
                ? new Warrior(type, name, author, warriorLines.ToArray(), warriorEndLine)
                : new Warrior(type, name, author, warriorLines.ToArray());
        }

        public string ToShortString()
        {
            string output = CodeLines.Aggregate("", (current, line) => current + (line + "\n"));
            if (HasEndLine)
                output += EndLine.ToString();
            return output;
        }

        public override string ToString()
        {
            return ";redcode-" + Type + "\n;name " + Name + "\n;author " + Author + "\n" + ToShortString();
        }

        public Warrior Clone()
        {
            WarriorLine[] lines = GenericCopier<WarriorLine[]>.DeepCopy(CodeLines);
            WarriorEndLine endLine = null;
            if (HasEndLine)
                endLine = GenericCopier<WarriorEndLine>.DeepCopy(EndLine);
            return HasEndLine
                ? new Warrior((string) Type.Clone(), (string) Name.Clone(), (string) Author.Clone(), lines, endLine)
                : new Warrior((string) Type.Clone(), (string) Name.Clone(), (string) Author.Clone(), lines);
        }

        #region Equal

        public bool Equals(Warrior other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Type, other.Type) && string.Equals(Name, other.Name) && string.Equals(Author, other.Author) && Equals(CodeLines, other.CodeLines) && Equals(EndLine, other.EndLine);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Warrior) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Type != null ? Type.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (CodeLines != null ? CodeLines.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (EndLine != null ? EndLine.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

    }
}
