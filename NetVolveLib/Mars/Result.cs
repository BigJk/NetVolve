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
            if (attackerWon == -1 && defenderWon == -1 && draw == -1) // <- Error
            {
                attackerWon = 0;
                defenderWon = 0;
                draw = 0;
            }
            AttackerWon = attackerWon;
            AttackerDraw = draw;
            DefenderDraw = draw;
            DefenderWon = defenderWon;
        }

    }
}
