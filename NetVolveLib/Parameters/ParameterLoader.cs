using System.IO;
using System.Web.Script.Serialization;
using NetVolveLib.Evolver;
using NetVolveLib.Redcode;
using NetVolveLib.Redcode.Enums;

namespace NetVolveLib.Parameters
{
    public static class ParameterLoader
    {

        public static Parameter FromFile(string path)
        {
            JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
            Parameter p = scriptSerializer.Deserialize<Parameter>(File.ReadAllText(path));
            string[] instructors = p.EvolverParameters.UsedInstructors.Split(' ');
            p.EvolverParameters.Instructors = new Instructors[instructors.Length];
            for (int i = 0; i < instructors.Length; i++)
            {
                p.EvolverParameters.Instructors[i] = Parser.IdentifyInstructor(instructors[i]);
            }
            Generator.UsedInstructorses = p.EvolverParameters.Instructors;
            return p;
        }

    }
}
