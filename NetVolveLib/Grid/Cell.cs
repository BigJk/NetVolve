using System;

namespace NetVolveLib.Grid
{
    [Serializable]
    public class Cell : IEquatable<Cell>
    {

        public Object CellLock = new Object();

        public GridWarrior Owner { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell(GridWarrior owner, int x, int y)
        {
            CellLock = new object();
            Owner = owner;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Changes the owner of the cell
        /// </summary>
        /// <param name="newOwner">New owner</param>
        public void ChangeOwner(GridWarrior newOwner)
        {
            Owner.OwnedCells.Remove(this);
            newOwner.OwnedCells.Add(this);
            Owner = newOwner;
        }

        #region Equal

        public bool Equals(Cell other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Cell) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X*397) ^ Y;
            }
        }

        #endregion

    }
}
