using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetVolveLib.Evolver;
using NetVolveLib.Mars;
using NetVolveLib.Name;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Utility;

namespace NetVolveLib.Grid
{
    [Serializable]
    public class Grid
    {

        public Cell[, ] Cells { get; set; }

        private readonly object _warriorLocker = new object();

        public List<GridWarrior> Warriors { get; set; }

        public int Size { get { return Parameters.GridParameters.Size; } }
        public Parameter Parameters { get; set; }

        private readonly Simulator _usedSimulator;
        private Task[] _tasks;
        private readonly Evolver.Evolver _evolver;
        private bool _run;

        public Grid(Parameter parameters)
        {     
            Parameters = parameters;

            Cells = new Cell[Parameters.GridParameters.Size, Parameters.GridParameters.Size];
            Warriors = new List<GridWarrior>();

            _usedSimulator = new Simulator(parameters);
            _evolver = new Evolver.Evolver(parameters);
            _run = false;

            ClearCache();
            FillGrid();
        }

        public Grid(Parameter parameters, Cell[,] cells, List<GridWarrior> warriors)
        {
            Parameters = parameters;

            Cells = cells;
            Warriors = warriors;

            _usedSimulator = new Simulator(parameters);
            _evolver = new Evolver.Evolver(parameters);
            _run = false;

            ClearCache();
        }

        public void ClearCache()
        {
            foreach (string s in Directory.GetFiles(Parameters.MarsParameters.Cache))
            {
                File.Delete(s);
            }
        }

        public void FillGrid()
        {
            if (!Directory.Exists("preset")) Directory.CreateDirectory("preset");
            List<Warrior> presWarriors =
                Directory.GetFiles("preset", "*.red")
                    .Select(Parser.WarriorFromOriginal)
                    .ToList();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Cells[y, x] = new Cell(x, y);
                    if (Statics.MainRandom.NextDouble() < Parameters.GridParameters.PresetChance && presWarriors.Count > 0)
                    {
                        GridWarrior newWarrior = new GridWarrior(presWarriors[0], Cells[y, x]);
                        newWarrior.Warrior.Name += "PRE";
                        Warriors.Add(newWarrior);
                        presWarriors.RemoveRange(0, 1);
                    }
                    else
                    {
                        Warrior newWarrior = Generator.GenerateRandomWarrior(Parameters.MarsParameters.Type,
                            NameGenerator.GetName(),
                            "NetVolve", Statics.MainRandom.Next(1, Parameters.MarsParameters.MaxWarriorLen), true,
                            Parameters.MarsParameters.Coresize);
                        GridWarrior newGridWarrior = new GridWarrior(newWarrior, Cells[y, x]);
                        Warriors.Add(newGridWarrior);
                    }
                }
            }
        }

        public void StopAsync()
        {
            _run = false;
            Task.WaitAll(_tasks.ToArray(), 2000);
        }

        public void StartAsync(int threads)
        {
            _run = true;
            Action a = () =>
            {
                while (_run)
                {
                    Step();
                    Thread.Sleep(1);
                }
            };
            _tasks = new Task[threads];     
            for (int i = 0; i < threads; i++)
            {
                _tasks[i] = Task.Factory.StartNew(a);
            }
        }

        public void CleanWarriors()
        {
            lock (_warriorLocker)
            {
                Warriors.RemoveAll(w => w.OwnedCells.Count == 0);
            }
        }

        public GridWarrior[] GetWarriors(int count)
        {
            GridWarrior[] output;
            lock (_warriorLocker)
            {
                Warriors.Sort();
                if (count > Warriors.Count) count = Warriors.Count;
                output = Warriors.GetRange(0, count).ToArray();
            }
            return output;
        }

        public void Step()
        {
            List<Fight> fights = new List<Fight>();

            Cell selectedCell = Cells[Statics.MainRandom.Next(Size), Statics.MainRandom.Next(Size)];

            fights.Add(GetFight(selectedCell, 1, 0));
            fights.Add(GetFight(selectedCell, 0, 1));
            fights.Add(GetFight(selectedCell, -1, 0));
            fights.Add(GetFight(selectedCell, 0, -1));

            fights.RemoveAll(x => x == null);

            foreach (Fight f in fights)
            {
                f.Simulate();
            }
        }

        private void FightCallback(Cell won, Cell lose)
        {
            lock (_warriorLocker)
            {
                GridWarrior loser = lose.Owner;
                if (Statics.MainRandom.NextDouble() < Parameters.EvolverParameters.EvolutionChance)
                {
                    GridWarrior newWarrior =
                        new GridWarrior(_evolver.EvolveWarrior(won.Owner.Warrior, lose.Owner.Warrior),
                            ColorHelper.IncreaseColor(won.Owner.Color, Statics.MainRandom.Next(-25, 25)));
                    Warriors.Add(newWarrior);
                    lose.ChangeOwner(newWarrior);
                }
                else
                {
                    lose.ChangeOwner(won.Owner);
                }
                if (loser.OwnedCells.Count == 0)
                {
                    Warriors.Remove(loser);
                }
            }
        }

        public Fight GetFight(Cell cell, int deltaX, int deltaY)
        {
            if (cell.X + deltaX < 0) deltaX = Size - 1;
            if (cell.Y + deltaY < 0) deltaY = Size - 1;
            if (cell.X + deltaX >= Size) deltaX = -(Size - 1);
            if (cell.Y + deltaY >= Size) deltaY = -(Size - 1);

            return !Equals(Cells[cell.Y + deltaY, cell.X + deltaX].Owner, cell.Owner)
                ? new Fight(cell, Cells[cell.Y + deltaY, cell.X + deltaX], _usedSimulator, FightCallback)
                : null;
        }

    }
}
