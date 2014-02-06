using System.Linq;

namespace NetVolveLib.Mars
{
    public class Result
    {

        public int AttackerWon { get; private set; }
        public int AttackerDraw { get; private set; }
        public int AttackerLose
        {
            get { return DefenderWon; }
        }

        public int DefenderWon { get; set; }
        public int DefenderDraw { get; set; }
        public int DefenderLose
        {
            get { return AttackerWon; }
        }

        public Result(int attackerWon, int defenderWon, int draw)
        {
            AttackerWon = attackerWon;
            AttackerDraw = draw;
            DefenderDraw = draw;
            DefenderWon = defenderWon;
        }

        internal static Result Parse(string s)
        {
            if (s.Contains("load"))
                return new Result(0, 0, 0);
            string[][] parts = s.Split('\n').Select(t => t.Split(' ')).ToArray();
            return new Result(int.Parse(parts[0][0]), int.Parse(parts[1][0]), int.Parse(parts[1][1]));
        }

    }
}
