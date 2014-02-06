using NetVolveLib.Parameters;
using NetVolveLib.Redcode;

namespace NetVolveLib.Evolver
{
    public interface IEvolverPlugin
    {

        string Name { get; }
        string Author { get; }
        string Description { get; }
        double Chance { get; set; }

        Warrior Execute(Warrior father, Warrior mother, Parameter parameter);

    }
}
