using NetVolveLib.Redcode.Lines;
using NetVolveLib.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetVolveLib.Redcode
{
    [Serializable]
    public class Warrior : IEquatable<Warrior>
    {

        public string Type { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public int Generation { get; set; }

        public WarriorLine[] CodeLines { get; set; }
        public WarriorEndLine EndLine { get; set; }

        public bool HasEndLine
        {
            get { return EndLine != null; }
        }

        public byte[] Genom
        {
            get
            {
                List<byte> genom = new List<byte>();

                foreach(WarriorLine w in CodeLines)
                {
                    genom.AddRange(new byte[] {
                        (byte)w.AddressingMode1,
                        (byte)w.AddressingMode2,
                        (byte)w.Instructor,
                        (byte)w.Modifier });

                    genom.AddRange(BitConverter.GetBytes(w.Number1));
                    genom.AddRange(BitConverter.GetBytes(w.Number2));
                }

                if (HasEndLine)
                {
                    genom.AddRange(BitConverter.GetBytes(EndLine.Number));
                }

                return genom.ToArray();
            }
        }
        
        public Warrior(string type, string name, string author, int generation, WarriorLine[] codeLines)
            : this(type, name, author, generation, codeLines, null) { }

        public Warrior(string type, string name, string author, WarriorLine[] codeLines)
            : this(type, name, author, codeLines, null) { }

        public Warrior(string type, string name, string author, int generation, WarriorLine[] codeLines, WarriorEndLine endLine)
            : this(type, name, author, codeLines, endLine)
        {
            Generation = generation;
        }

        public Warrior(string type, string name, string author, WarriorLine[] codeLines, WarriorEndLine endLine)
        {
            Type = type;
            Name = name;
            Author = author;
            CodeLines = codeLines;
            EndLine = endLine;
        }

        /// <summary>
        /// Saves warrior to file
        /// </summary>
        /// <param name="path">Path to file</param>
        public void Save(string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
            {
                using (BinaryWriter binWriter = new BinaryWriter(fileStream))
                {
                    binWriter.Write(Type);
                    binWriter.Write(Name);
                    binWriter.Write(Author);
                    binWriter.Write(Generation);
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

        /// <summary>
        /// Loads warrior from file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns></returns>
        public static Warrior Load(string path)
        {
            string type, name, author;
            int generation;
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
                    generation = binReader.ReadInt32();
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
                ? new Warrior(type, name, author, generation, warriorLines.ToArray(), warriorEndLine)
                : new Warrior(type, name, author, generation, warriorLines.ToArray());
        }

        /// <summary>
        /// Returns redcode of warrior without other informations like name or author
        /// </summary>
        /// <returns></returns>
        public string ToShortString()
        {
            string output = CodeLines.Aggregate("", (current, line) => current + (line + "\n"));
            if (HasEndLine)
                output += EndLine.ToString();
            return output;
        }

        /// <summary>
        /// Returns redcode of warrior
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ";redcode-" + Type + "\n;name " + Name + "\n;author " + Author + "\n" + ToShortString();
        }

        /// <summary>
        /// Calculates name by generation and crc32 of the genom
        /// </summary>
        public void SetWarriorName()
        {
            Name = Generation + "-" + Crc32.Compute(Genom).ToString("x2").ToUpper();
        }

        /// <summary>
        /// Creates a deep copy of the warrior
        /// </summary>
        /// <returns></returns>
        public Warrior DeepCopy()
        {
            char[] type = new char[Type.Length];
            char[] name = new char[Name.Length];
            char[] author = new char[Author.Length];

            Type.CopyTo(0, type, 0, Type.Length);
            Name.CopyTo(0, name, 0, Name.Length);
            Author.CopyTo(0, author, 0, Author.Length);

            return HasEndLine
                ? new Warrior(type.ToString(), name.ToString(), author.ToString(), Generation, CodeLines.Select(warriorLine => warriorLine.DeepCopy()).ToArray(), EndLine.DeepCopy())
                : new Warrior(type.ToString(), name.ToString(), author.ToString(), Generation, CodeLines.Select(warriorLine => warriorLine.DeepCopy()).ToArray());
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
