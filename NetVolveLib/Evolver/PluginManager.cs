using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NetVolveLib.Evolver.Plugins;
using NetVolveLib.Parameters;

namespace NetVolveLib.Evolver
{
    public class PluginManager
    {

        public List<IEvolverPlugin> Plugins { get; set; }

        public IEvolverPlugin this[int i]
        {
            get { return Plugins[i]; }
        }

        public IEvolverPlugin this[string s]
        {
            get { return Plugins.FirstOrDefault(plugin => plugin.Name == s); }
        }

        public PluginManager(Parameter parameter)
        {
            // Basic hardcoded plugins
            Plugins = new List<IEvolverPlugin>
            {
                new AddLine(),
                new ChangeAddressingModes(),
                new ChangeEnd(),
                new ChangeInstructors(),
                new ChangeModifiers(),
                new ChangeNumbers(),
                new Crossover(),
                new RemoveLine()
            };

            LoadPlugins();

            foreach (PluginChance pluginChance in parameter.EvolverParameters.Plugins)
            {
                this[pluginChance.Name].Chance = pluginChance.Chance;
            }
        }

        /// <summary>
        /// Loads all plugins from the plugin directory
        /// </summary>
        public void LoadPlugins()
        {
            if (!Directory.Exists("plugins")) Directory.CreateDirectory("plugins");
            foreach (string plugin in Directory.GetFiles("plugins", "*.dll"))
            {
                Assembly asm = Assembly.LoadFrom(plugin);
                foreach (Type type in asm.GetTypes())
                {
                    if (type.GetInterfaces().Contains(typeof(IEvolverPlugin)))
                    {
                        Plugins.Add((IEvolverPlugin)Activator.CreateInstance(type));
                    }
                }
            }
        }

    }
}
