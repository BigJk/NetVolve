using System;
using System.Diagnostics;
using System.IO;
using NetVolveLib.Redcode;

namespace NetVolveLib.Mars
{
    [Serializable]
    public class Simulator
    {

        public Parameters.Parameter Parameters { get; set; }

        public string ParameterString
        {
            get
            {
                return "-r " + Parameters.MarsParameters.Rounds + " -c " + Parameters.MarsParameters.Cycles + " -s " + Parameters.MarsParameters.Coresize + " -p " +
                       Parameters.MarsParameters.MaxProcess + " -l " + Parameters.MarsParameters.MaxWarriorLen + " -d " + Parameters.MarsParameters.MaxWarriorLen;
            }
        }

        public Simulator(Parameters.Parameter parameters)
        {
            Parameters = parameters;
        }

        public Result SimulateFight(Warrior attacker, Warrior defender)
        {
            string attackerPath, defenderPath;
            lock (Statics.CounterLocker)
            {
                attackerPath = Parameters.MarsParameters.Cache + "/" + ++Statics.Counter + ".c";
                defenderPath = Parameters.MarsParameters.Cache + "/" + ++Statics.Counter + ".c";
                if (Statics.Counter > 100000) Statics.Counter = 0;
            }

            File.WriteAllText(attackerPath, attacker.ToShortString());
            File.WriteAllText(defenderPath, defender.ToShortString());

            Result output = SimulateFight(attackerPath, defenderPath);

            File.Delete(attackerPath);
            File.Delete(defenderPath);

            return output;
        }

        public Result SimulateFight(string attackerPath, string defenderPath)
        {
            Process marsProcess = new Process
            {
                StartInfo =
                    new ProcessStartInfo(Parameters.MarsParameters.Exmars, ParameterString + " \"" + attackerPath + "\" \"" + defenderPath + "\"")
                    {
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
            };

            marsProcess.Start();

            string output = "";
            using (StreamReader streamReader = marsProcess.StandardOutput)
            {
                while (!streamReader.EndOfStream)
                    output += streamReader.ReadLine() + "\n";
            }

            return Result.Parse(output);
        }

    }
}
