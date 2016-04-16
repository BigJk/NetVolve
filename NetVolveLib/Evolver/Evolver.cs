using System.Collections.Generic;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;

namespace NetVolveLib.Evolver
{
    public class Evolver
    {

        public Parameter Parameters { get; set; }
        public Dictionary<string, IEvolverPlugin> Plugins;
        public PluginManager PluginManager;

        public Evolver(Parameter parameters)
        {
            Parameters = parameters;
            PluginManager = new PluginManager(parameters);
        }

        /// <summary>
        /// Let's the warrior evolve with the posibility to use buddy as a partner
        /// </summary>
        /// <param name="warrior">Main warrior</param>
        /// <param name="buddy">Possible mutation partner</param>
        /// <param name="crossover">True if crossover happend</param>
        /// <returns></returns>
        public Warrior EvolveWarrior(Warrior warrior, Warrior buddy, ref bool crossover)
        {
            Warrior cWarrior = warrior.DeepCopy();
            Warrior cBuddy = buddy.DeepCopy();

            int changes = 0;
            while(changes < Statics.MainRandom.Next(1,6))
            {
                IEvolverPlugin selectedPlugin;
                while (true)
                {
                    selectedPlugin = PluginManager[Statics.MainRandom.Next(PluginManager.Plugins.Count)];
                    if (!(selectedPlugin is IEvolverPluginExtended)) { break; }
                    IEvolverPluginExtended extendedPlugin = (IEvolverPluginExtended) selectedPlugin;
                    if (extendedPlugin.Possible(warrior, Parameters)) { break; }
                }

                if (!(Statics.MainRandom.NextDouble() > selectedPlugin.Chance)) continue;
                if (selectedPlugin.Name == "Crossover")
                {
                    crossover = true;
                }
                cWarrior = selectedPlugin.Execute(cWarrior, cBuddy, Parameters);
                changes++;
            }

            cWarrior.Generation++;
            cWarrior.Author = "NetVolve";
            cWarrior.SetWarriorName();
            return cWarrior;
        }

    }
}
