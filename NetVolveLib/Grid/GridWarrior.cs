using System;
using System.Collections.Generic;
using System.Drawing;
using NetVolveLib.Redcode;

namespace NetVolveLib.Grid
{
    [Serializable]
    public class GridWarrior : IEquatable<GridWarrior>, IComparable<GridWarrior>
    {

        public Warrior Warrior { get; set; }

        public int Wins { get; set; }
        public int Lose { get; set; }

        public Color Color { get; set; }

        public List<Cell> OwnedCells { get; set; }

        public GridWarrior(Warrior warrior, Color color)
        {
            OwnedCells = new List<Cell>();
            Warrior = warrior;
            Color = color;
        }

        public GridWarrior(Warrior warrior)
            : this(warrior, Color.FromArgb(Statics.MainRandom.Next(256), Statics.MainRandom.Next(256), Statics.MainRandom.Next(256)))
        {
            Warrior = warrior;
        }

        public GridWarrior(Warrior warrior, Cell startCell) : this(warrior)
        {
            OwnedCells.Add(startCell);
            startCell.Owner = this;
        }

        public GridWarrior(Warrior warrior, Cell startCell, Color color) : this(warrior, color)
        {
            OwnedCells.Add(startCell);
            startCell.Owner = this;
        }

        /// <summary>
        /// Creates a deep copy of the 'GridWarrior'
        /// </summary>
        /// <returns></returns>
        public GridWarrior Clone()
        {
            return new GridWarrior(Warrior.DeepCopy(), Color);
        }

        public int CompareTo(GridWarrior other)
        {
            return other.OwnedCells.Count.CompareTo(OwnedCells.Count);
        }

        #region Equal

        public bool Equals(GridWarrior other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Warrior, other.Warrior) && Wins == other.Wins && Lose == other.Lose && Color.Equals(other.Color) && Equals(OwnedCells, other.OwnedCells);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GridWarrior) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Warrior != null ? Warrior.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Wins;
                hashCode = (hashCode*397) ^ Lose;
                hashCode = (hashCode*397) ^ Color.GetHashCode();
                hashCode = (hashCode*397) ^ (OwnedCells != null ? OwnedCells.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

    }
}
