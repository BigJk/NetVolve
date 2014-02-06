using System;
using System.Threading;
using NetVolveLib.Mars;

namespace NetVolveLib.Grid
{
    public class Fight
    {

        public Simulator Simulator { get; set; }

        public GridWarrior Attacker
        {
            get { return AttackerPosition.Owner; }
        }
        public Cell AttackerPosition { get; set; }

        public GridWarrior Defender
        {
            get { return DefenderPosition.Owner; }
        }
        public Cell DefenderPosition { get; set; }

        public Result Result { get; set; }
        public bool Finished
        {
            get { return Result != null; }
        }

        private readonly Action<Cell, Cell> _fightCallback;

        public Fight(Cell attackerPosition, Cell defenderPosition, Simulator simulator, Action<Cell, Cell> fightFinished)
        {
            AttackerPosition = attackerPosition;
            DefenderPosition = defenderPosition;
            Simulator = simulator;
            _fightCallback = fightFinished;
        }

        public void Simulate()
        {
            bool finished = false;
            while (!finished)
            {
                bool aLock = Monitor.TryEnter(AttackerPosition.CellLock, 100);
                bool dLock = Monitor.TryEnter(DefenderPosition.CellLock, 100);

                if (aLock && dLock)
                {
                    Result = Simulator.SimulateFight(Attacker.Warrior, Defender.Warrior);

                    if (Result.AttackerWon > Result.DefenderWon)
                    {
                        Attacker.Wins++;
                        Defender.Lose++;
                        _fightCallback(AttackerPosition, DefenderPosition);
                    }
                    else if (Result.DefenderWon > Result.AttackerWon)
                    {
                        Attacker.Lose++;
                        Defender.Wins++;
                        _fightCallback(DefenderPosition, AttackerPosition);
                    }

                    finished = true;
                }

                if (aLock)
                    Monitor.Exit(AttackerPosition.CellLock);
                if (dLock)
                    Monitor.Exit(DefenderPosition.CellLock);
            }
        }

        public GridWarrior GetWinner()
        {
            return Result.AttackerWon > Result.DefenderWon ? Attacker : Defender;
        }
    }
}
