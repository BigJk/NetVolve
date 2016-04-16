using System.Diagnostics;
using System.Drawing;

namespace NetVolveLib.Utility
{
    public static class ColorHelper
    {

        public static Color IncreaseColor(Color baseColor, int r, int g, int b)
        {
            return Color.FromArgb(Change(baseColor.R, r), Change(baseColor.G, g), Change(baseColor.B, b));
        }

        private static int Change(int number, int amount)
        {
            if (number + amount > 255 || number + amount < 0)
                amount = -amount;
            return number + amount;
        }

        public static Color Mix(Color aColor, Color bColor)
        {
            float r = (aColor.R + bColor.R) / (2.0f);
            float g = (aColor.G + bColor.G) / (2.0f);
            float b = (aColor.B + bColor.B) / (2.0f);
            return Color.FromArgb((byte)r, (byte)g, (byte)b);
        }

    }
}
