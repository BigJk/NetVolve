using System;

namespace NetVolveCGUI
{
    [Serializable]
    class WarriorColor
    {

        public int Color { get; set; }
        public char Char { get; set; }

        private static char[] _possible = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890#+-?!§$%&=~/\\^".ToCharArray();
        private static Random _random = new Random();

        public WarriorColor()
        {
            Color = _random.Next(256);
            Char = _possible[_random.Next(_possible.Length)];
        }

    }
}
