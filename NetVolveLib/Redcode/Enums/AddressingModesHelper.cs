namespace NetVolveLib.Redcode.Enums
{
    public static class AddressingModesHelper
    {

        public static string GetString(AddressingModes m)
        {
            switch (m)
            {
                case AddressingModes.at:
                    return "@";
                case AddressingModes.close:
                    return "}";
                case AddressingModes.dollar:
                    return "$";
                case AddressingModes.higherThen:
                    return ">";
                case AddressingModes.lowerThen:
                    return "<";
                case AddressingModes.open:
                    return "{";
                case AddressingModes.sharp:
                    return "#";
                case AddressingModes.star:
                    return "*";
            }
            return "";
        }

    }
}
