﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NetVolveLib.Parameters;

namespace NetVolveLib.Grid
{
    public static class GridSerializer
    {

        /// <summary>
        /// Saves the grid to file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="grid">Instance of grid</param>
        public static void Save(string path, Grid grid)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate,FileAccess.Write, FileShare.None))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, grid.Parameters);
                binaryFormatter.Serialize(fileStream, grid.Cells);
            }
        }

        /// <summary>
        /// Loads the grid from file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns></returns>
        public static Grid Load(string path)
        {
            Parameter paras;
            Cell[,] cells;
            List<GridWarrior> warriors = new List<GridWarrior>();
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                paras = (Parameter)binaryFormatter.Deserialize(fileStream);
                cells = (Cell[,])binaryFormatter.Deserialize(fileStream);
            }
            foreach (Cell c in cells)
            {
                if (!warriors.Contains(c.Owner))
                    warriors.Add(c.Owner);
            }
            return new Grid(paras, cells, warriors);
        }

    }
}
