using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NetVolveLib.Evolver;
using NetVolveLib.Mars;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Utility;

namespace NetVolveLib.Grid
{
    [Serializable]
    public class Grid
    {

        public Cell[,] Cells { get; set; }

        private readonly object _warriorLocker = new object();

        public List<GridWarrior> Warriors { get; set; }
        public ConcurrentQueue<Warrior> Sleeper { get; set; }

        public int Size { get { return Parameters.GridParameters.Size; } }
        public Parameter Parameters { get; set; }

        public DateTime StartingTime { get; set; }
        public int FightsDone { get; set; }
        public int Fps
        {
            get { return (int)Math.Round(FightsDone/(DateTime.Now - StartingTime).TotalSeconds); }
        }

        private readonly Simulator _usedSimulator;
        private Task[] _tasks;
        private readonly Evolver.Evolver _evolver;
        private bool _run;

        public Grid(Parameter parameters)
        {
            StartingTime = DateTime.Now;
            Parameters = parameters;

            Sleeper = new ConcurrentQueue<Warrior>();
            Cells = new Cell[Parameters.GridParameters.Size, Parameters.GridParameters.Size];
            Warriors = new List<GridWarrior>();

            _usedSimulator = new Simulator(parameters);
            _evolver = new Evolver.Evolver(parameters);
            _run = false;

            FillGrid();
        }

        public Grid(Parameter parameters, Cell[,] cells, List<GridWarrior> warriors)
        {
            StartingTime = DateTime.Now;
            Parameters = parameters;

            Sleeper = new ConcurrentQueue<Warrior>();
            Cells = cells;
            Warriors = warriors;

            _usedSimulator = new Simulator(parameters);
            _evolver = new Evolver.Evolver(parameters);
            _run = false;
        }

        /// <summary>
        /// Fills the Grid with warriors random preset or newly generated warriors
        /// </summary>
        public void FillGrid()
        {
            if (!Directory.Exists("preset")) Directory.CreateDirectory("preset");
            List<Warrior> presWarriors = Directory.GetFiles("preset", "*.red").Select(Parser.WarriorFromOriginal).ToList();

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    Cells[y, x] = new Cell(x, y);
                    if (Statics.MainRandom.NextDouble() < Parameters.GridParameters.PresetChance && presWarriors.Count > 0)
                    {
                        GridWarrior newWarrior = new GridWarrior(presWarriors[0], Cells[y, x]);
                        Warriors.Add(newWarrior);
                        presWarriors.RemoveRange(0, 1);
                    }
                    else
                    {
                        Warrior newWarrior = Generator.GenerateRandomWarrior(
                            Parameters.MarsParameters.Type,
                            "",
                            "NetVolve",
                            Statics.MainRandom.Next(1, Parameters.MarsParameters.MaxWarriorLen),
                            true,
                            Parameters.MarsParameters.Coresize);

                        newWarrior.SetWarriorName();
                        GridWarrior newGridWarrior = new GridWarrior(newWarrior, Cells[y, x]);
                        Warriors.Add(newGridWarrior);
                    }
                }
            }
        }

        /// <summary>
        /// Stops all the working thread's
        /// </summary>
        public void StopAsync()
        {
            _run = false;
            Task.WaitAll(_tasks.ToArray(), 2000);
        }


        /// <summary>
        /// Starts multiple threads to work
        /// </summary>
        /// <param name="threads">Amount of working threads</param>
        public void StartAsync(int threads)
        {
            StartingTime = DateTime.Now;
            _run = true;
            Action a = () =>
            {
                while (_run)
                {
                    Step();
                    Thread.Sleep(1);
                }
            };
            _tasks = new Task[threads + 1];     
            for (int i = 0; i < threads; i++)
            {
                _tasks[i] = Task.Factory.StartNew(a);
            }
            _tasks[threads] = Task.Factory.StartNew(() =>
            {
                while (_run)
                {
                    StartingTime = DateTime.Now;
                    FightsDone = 0;
                    Thread.Sleep(5000);
                }
            });
        }

        /// <summary>
        /// Removes all dead warriors
        /// </summary>
        public void CleanWarriors()
        {
            lock (_warriorLocker)
            {
                Warriors.RemoveAll(w => w.OwnedCells.Count == 0);
            }
        }

        /// <summary>
        /// Get's the the best 'count' warrior's
        /// </summary>
        /// <param name="count">The amount of warriors to get</param>
        /// <returns></returns>
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

        /// <summary>
        /// Check's if sleeper's need to be saved and save's them
        /// </summary>
        public void CheckForSleeper()
        {

            if (Parameters.GridParameters.SleeperAmount == 0) return;
            if (FightsDone%Parameters.GridParameters.SleeperRate > 0) return;

            Warriors.Sort();
            for (int i = 0; i < Parameters.GridParameters.SleeperAmount; i++)
            {
                Warrior w = Warriors[i].Warrior.DeepCopy();
                w.Name += "-S";
                Sleeper.Enqueue(w);
            }

        }

        /// <summary>
        /// Check's if warrior's need to be replaced and replace them
        /// </summary>
        public void CheckForReplace()
        {
            if (Parameters.GridParameters.ReplaceAmount <= 0 || FightsDone % Parameters.GridParameters.ReplaceRate != 0)
                return;

            for (int i = 0; i < Parameters.GridParameters.ReplaceAmount; i++)
            {

                int x, y;
                int tries = 0;
                while (true)
                {
                    tries++;
                    x = Statics.MainRandom.Next(Size);
                    y = Statics.MainRandom.Next(Size);
                    if (Cells[x, y].Owner.OwnedCells.Count() > 5)
                    {
                        break;
                    }
                    if (tries < 100) continue;
                    return;
                }

                string[] presets = Directory.GetFiles("preset");
                if (presets.Any() && Statics.MainRandom.NextDouble() < Parameters.GridParameters.ReplacePresetChance)
                {
                    Cells[x, y].ChangeOwner(new GridWarrior(Parser.WarriorFromOriginal(presets[Statics.MainRandom.Next(presets.Count())])));
                }
                else if (Sleeper.Any() && Statics.MainRandom.NextDouble() < Parameters.GridParameters.ReplaceSleeperChance)
                {
                    Warrior w;
                    while (Sleeper.TryDequeue(out w))
                        Cells[x, y].ChangeOwner(new GridWarrior(w));
                }
                else
                {
                    Cells[x, y].ChangeOwner(new GridWarrior(Generator.GenerateRandomWarrior(
                        Parameters.MarsParameters.Type,
                        "",
                        "NetVolve",
                        Statics.MainRandom.Next(1, Parameters.MarsParameters.MaxWarriorLen - 1),
                        true,
                        Parameters.MarsParameters.Coresize)));

                    Cells[x, y].Owner.Warrior.SetWarriorName();
                }

            }
        }

        /// <summary>
        /// Selects random Cell and let it fight agains his neighbor's
        /// </summary>
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

        /// <summary>
        /// Get's called when a fight is finished
        /// </summary>
        /// <param name="won">The cell that won</param>
        /// <param name="lose">The cell that lost</param>
        private void FightCallback(Cell won, Cell lose)
        {
            FightsDone++;
            lock (_warriorLocker)
            {

                // Call's check function's to see if interval has passed
                CheckForSleeper();
                CheckForReplace();

                GridWarrior loser = lose.Owner;
                if (Statics.MainRandom.NextDouble() < Parameters.EvolverParameters.EvolutionChance)
                {
                    bool crossover = false;
                    Warrior evolveWarrior = _evolver.EvolveWarrior(won.Owner.Warrior, lose.Owner.Warrior, ref crossover);
                    GridWarrior newWarrior = new GridWarrior(
                        evolveWarrior,
                        !crossover ? ColorHelper.IncreaseColor(won.Owner.Color, Statics.MainRandom.Next(-25, 25), Statics.MainRandom.Next(-25, 25), Statics.MainRandom.Next(-25, 25)) : ColorHelper.Mix(won.Owner.Color, lose.Owner.Color));
                            

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

        /// <summary>
        /// Get's cell
        /// </summary>
        /// <param name="cell">Source cell</param>
        /// <param name="deltaX">Horizontal distance</param>
        /// <param name="deltaY">Vertical distance</param>
        /// <returns></returns>
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
