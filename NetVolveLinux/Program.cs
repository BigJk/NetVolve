using System;
using System.Threading;
using NetVolveLib.Evolver;
using NetVolveLib.Grid;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;

namespace NetVolveLinux
{
    class Program
    {
        static void Main(string[] args)
        {

            Parameter p = new Parameter
            {
                MarsParameters = new MarsParameter
                {
                    Type = "tiny",
                    Exmars = "exmars",
                    Cache = "c",
                    Coresize = 800,
                    MaxProcess = 800,
                    Cycles = 800,
                    Rounds = 50,
                    MaxWarriorLen = 20
                },
                EvolverParameters = new EvolverParameter
                {
                    EvolutionChance = 0.4,
                    Plugins = new PluginChance[]
                    {
                        new PluginChance() {Chance = 0.2, Name = "ChangeInstructors"},
                        new PluginChance() {Chance = 0.2, Name = "ChangeModifiers"},
                        new PluginChance() {Chance = 0.2, Name = "ChangeAddressingModes"},
                        new PluginChance() {Chance = 0.2, Name = "ChangeNumbers"},
                        new PluginChance() {Chance = 0.2, Name = "ChangeEnd"},
                        new PluginChance() {Chance = 0.2, Name = "Crossover"},
                        new PluginChance() {Chance = 0.2, Name = "RemoveLine"},
                        new PluginChance() {Chance = 0.2, Name = "AddLine"}
                    }
                },
                GridParameters = new GridParameter
                {
                    Size = 10,
                    PresetChance = 0.0
                }
            };

            string[] instructors = "spl spl spl spl spl mov mov mov mov dat dat dat jmp jmn jmz djn sne seq add sub mul div mod".Split(' ');
            p.EvolverParameters.Instructors = new Instructors[instructors.Length];
            for (int i = 0; i < instructors.Length; i++)
            {
                p.EvolverParameters.Instructors[i] = Parser.IdentifyInstructor(instructors[i]);
            }
            Generator.UsedInstructorses = p.EvolverParameters.Instructors;

            Grid g = new Grid(p);

            while (true)
            {
                g.Step();
                GridWarrior w = g.GetWarriors(1)[0];
                Console.SetCursorPosition(0,0);
                Console.Write("Warrior {0} by {1} is #1 with {2} fields                                         ", w.Warrior.Name, w.Warrior.Author, w.OwnedCells.Count);
                Thread.Sleep(100);
            }

        }
    }
}
