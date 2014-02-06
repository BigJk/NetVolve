using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;

namespace NetVolveLib.Name
{
    public static class NameGenerator
    {

        private static readonly List<NameBase> AvailableBases;

        static NameGenerator()
        {
            AvailableBases = new List<NameBase>();
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            foreach (string s in Directory.GetFiles("data","*.lang"))
            {
                AvailableBases.Add(new NameBase(scriptSerializer.Deserialize<RawNameBase>(File.ReadAllText(s))));
            }
        }

        public static string GetName()
        {
            NameBase selectedBase = AvailableBases[Statics.MainRandom.Next(AvailableBases.Count)];

            string name = selectedBase.SyllablesStart[Statics.MainRandom.Next(selectedBase.SyllablesStart.Count())] +
                          selectedBase.PhonemesVocals[Statics.MainRandom.Next(selectedBase.PhonemesVocals.Count())] +
                          selectedBase.PhonemesConsonants[Statics.MainRandom.Next(selectedBase.PhonemesConsonants.Count())];

            for (int i = 0; i < Statics.MainRandom.Next(0, 3); i++) name += selectedBase.SyllablesMiddle[Statics.MainRandom.Next(selectedBase.SyllablesMiddle.Count())];

            name += selectedBase.SyllablesEnd[Statics.MainRandom.Next(selectedBase.SyllablesEnd.Count())];

            return name;
        }

    }
}
