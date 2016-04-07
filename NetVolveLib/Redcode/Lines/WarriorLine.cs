using System;
using System.IO;
using NetVolveLib.Redcode.Enums;

namespace NetVolveLib.Redcode.Lines
{
    [Serializable]
    public class WarriorLine : IEquatable<WarriorLine>
    {

        public Instructors Instructor { get; set; }
        public Modifiers Modifier { get; set; }
        public AddressingModes AddressingMode1 { get; set; }
        public AddressingModes AddressingMode2 { get; set; }
        public short Number1 { get; set; }
        public short Number2 { get; set; }

        public WarriorLine(Instructors instructor, Modifiers modifier, AddressingModes addressingMode1, AddressingModes addressingMode2, short number1, short number2)
        {
            Instructor = instructor;
            Modifier = modifier;
            AddressingMode1 = addressingMode1;
            AddressingMode2 = addressingMode2;
            Number1 = number1;
            Number2 = number2;
        }

        internal void WriteToStream(BinaryWriter binWriter)
        {
            binWriter.Write((byte)Instructor);
            binWriter.Write((byte)Modifier);
            binWriter.Write((byte)AddressingMode1);
            binWriter.Write((byte)AddressingMode2);
            binWriter.Write(Number1);
            binWriter.Write(Number2);
        }

        static internal WarriorLine ReadFromStream(BinaryReader binReader)
        {
            Instructors instructor = (Instructors)binReader.ReadByte();
            Modifiers modifier = (Modifiers)binReader.ReadByte();
            AddressingModes addressingMode1 = (AddressingModes)binReader.ReadByte();
            AddressingModes addressingMode2 = (AddressingModes)binReader.ReadByte();
            short number1 = binReader.ReadInt16();
            short number2 = binReader.ReadInt16();
            return new WarriorLine(instructor, modifier, addressingMode1, addressingMode2, number1, number2);
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(Instructors), Instructor) + "."
                + Enum.GetName(typeof(Modifiers), Modifier) + " "
                + AddressingModesHelper.GetString(AddressingMode1) + " "
                + Number1 + ", "
                + AddressingModesHelper.GetString(AddressingMode2) + " "
                + Number2;
        }

        public WarriorLine DeepCopy()
        {
            return new WarriorLine(Instructor, Modifier, AddressingMode1, AddressingMode2, Number1, Number2);
        }

        #region Equal

        public bool Equals(WarriorLine other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Instructor == other.Instructor && Modifier == other.Modifier && AddressingMode1 == other.AddressingMode1 && AddressingMode2 == other.AddressingMode2 && Number2 == other.Number2 && Number1 == other.Number1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((WarriorLine)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (int)Instructor;
                hashCode = (hashCode * 397) ^ (int)Modifier;
                hashCode = (hashCode * 397) ^ (int)AddressingMode1;
                hashCode = (hashCode * 397) ^ (int)AddressingMode2;
                hashCode = (hashCode * 397) ^ Number2.GetHashCode();
                hashCode = (hashCode * 397) ^ Number1.GetHashCode();
                return hashCode;
            }
        }

        #endregion

    }
}
