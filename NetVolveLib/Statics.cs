using System;

namespace NetVolveLib
{
    internal class Statics
    {
        public static Random MainRandom = new Random(DateTime.Now.Millisecond * 100);
        public static object CounterLocker = new object();
        public static int Counter = 0;
    }
}
