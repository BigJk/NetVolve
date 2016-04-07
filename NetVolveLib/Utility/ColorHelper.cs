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

    }
}
