using System;

namespace NetVolveLib.Parameters
{
    [Serializable]
    public class Parameter
    {
        public MarsParameter MarsParameters { get; set; }
        public EvolverParameter EvolverParameters { get; set; }
        public GridParameter GridParameters { get; set; }
    }
}
