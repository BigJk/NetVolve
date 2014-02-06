using System;
using System.Linq;

namespace NetVolveLib.Name
{
    class NameBase
    {

        public string[] PhonemesVocals { get; set; }
        public string[] PhonemesConsonants { get; set; }
        public string[] SyllablesStart { get; set; }
        public string[] SyllablesMiddle { get; set; }
        public string[] SyllablesEnd { get; set; }

        public NameBase(RawNameBase nameBase)
        {
            PhonemesConsonants = nameBase.phonemesConsonants.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            PhonemesVocals = nameBase.phonemesVocals.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            SyllablesStart = nameBase.syllablesStart.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            SyllablesMiddle = nameBase.syllablesMiddle.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            SyllablesEnd = nameBase.syllablesEnd.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            if (!PhonemesConsonants.Any()) PhonemesConsonants = new[] { "" };
            if (!PhonemesVocals.Any()) PhonemesVocals = new[] { "" };
            if (!SyllablesStart.Any()) SyllablesStart = new[] { "" };
            if (!SyllablesMiddle.Any()) SyllablesMiddle = new[] { "" };
            if (!SyllablesEnd.Any()) SyllablesEnd = new[] { "" };
        }

    }
}
