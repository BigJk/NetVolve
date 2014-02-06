using System.Drawing;

namespace NetVolveLib.Utility
{
    public static class ColorHelper
    {

        public static Color IncreaseColor(Color baseColor, int amount)
        {
            return Color.FromArgb(Change(baseColor.R, amount), Change(baseColor.G, amount), Change(baseColor.B, amount));
        }

        private static int Change(int number, int amount)
        {
            if (number + amount > 255 || number + amount < 0)
                amount = -amount;
            return number + amount;
        }

    }
}
