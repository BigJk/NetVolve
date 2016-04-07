using NetVolveLib.Parameters;
using NetVolveLib.Redcode;

namespace NetVolveLib.Evolver
{
    interface IEvolverPluginExtended : IEvolverPlugin
    {

        bool Possible(Warrior father, Parameter parameter); 

    }
}
