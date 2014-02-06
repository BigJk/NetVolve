using System.Collections.Generic;
using NetVolveLib.Name;
using NetVolveLib.Parameters;
using NetVolveLib.Redcode;
using NetVolveLib.Utility;

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

        public Warrior EvolveWarrior(Warrior warrior, Warrior buddy)
        {
            Warrior cWarrior = GenericCopier<Warrior>.DeepCopy(warrior);
            Warrior cBuddy = GenericCopier<Warrior>.DeepCopy(buddy);

            int changes = 0;
            while(changes < 5)
            {
                IEvolverPlugin selectedPlugin = PluginManager[Statics.MainRandom.Next(PluginManager.Plugins.Count)];
                if (!(Statics.MainRandom.NextDouble() > selectedPlugin.Chance)) continue;
                cWarrior = selectedPlugin.Execute(cWarrior, cBuddy, Parameters);
                changes++;
            }

            cWarrior.Name = NameGenerator.GetName();
            cWarrior.Author = "NetVolve";
            return cWarrior;
        }

    }
}
