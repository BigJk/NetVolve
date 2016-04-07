using System;
using System.Runtime.InteropServices;
using NetVolveLib.Redcode;

namespace NetVolveLib.Mars
{

    [Serializable]
    public class Simulator
    {

        [DllImport(@"exmars.dll", EntryPoint = "Fight2Warriors@40", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void Fight2Warriors(
            string w1, 
            string w2, 
            int coresize, 
            int cycles,
            int maxprocess,
            int rounds,
            int maxwarriorlen,
            ref int win1,
            ref int win2,
            ref int equal );

        public Parameters.Parameter Parameters { get; set; }

        public Simulator(Parameters.Parameter parameters)
        {
            Parameters = parameters;
        }

        /// <summary>
        /// Simulates a fight between two warrior's
        /// </summary>
        /// <param name="attacker">The attacking warrior</param>
        /// <param name="defender">The defending warrior</param>
        /// <returns></returns>
        public Result SimulateFight(Warrior attacker, Warrior defender)
        {
            int win1 = 0, win2 = 0, draw = 0;
            Fight2Warriors(
                attacker.ToShortString(),
                defender.ToShortString(),
                Parameters.MarsParameters.Coresize,
                Parameters.MarsParameters.Cycles,
                Parameters.MarsParameters.MaxProcess,
                Parameters.MarsParameters.Rounds,
                Parameters.MarsParameters.MaxWarriorLen,
                ref win1, ref win2, ref draw);
            return new Result(win1, win2, draw);
        }

    }
}
