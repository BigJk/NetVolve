using System;
using System.IO;

namespace NetVolveLib.Redcode.Lines
{
    [Serializable]
    public class WarriorEndLine : IEquatable<WarriorEndLine>
    {

        public readonly short Number;

        public WarriorEndLine(short number)
        {
            Number = number;
        }

        public override string ToString()
        {
            return "end " + Number;
        }

        internal void WriteToStream(BinaryWriter binWriter)
        {
            binWriter.Write(Number);
        }

        static internal WarriorEndLine ReadFromStream(BinaryReader binReader)
        {
            return new WarriorEndLine(binReader.ReadInt16());
        }

        public WarriorEndLine DeepCopy()
        {
            return new WarriorEndLine(Number);
        }

        #region Equal

        public bool Equals(WarriorEndLine other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Number == other.Number;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((WarriorEndLine)obj);
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        #endregion

    }
}
