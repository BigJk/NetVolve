using System;

namespace NetVolveLib
{
    internal class Statics
    {
        private static Random _mainRandom = new Random(DateTime.Now.Millisecond * 100);
        private static long _rCounter;

        private static readonly object RandomLock = new object();

        public static Random MainRandom
        {
            get
            {
                lock (RandomLock)
                {
                    _rCounter++;
                    if (_rCounter < 1000000) return _mainRandom;
                    _mainRandom = new Random(DateTime.Now.Millisecond * 100);
                    _rCounter = 0;
                }
                return _mainRandom;
            }   
        }
    }
}
