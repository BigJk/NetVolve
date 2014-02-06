using System;

namespace NetVolveLib.Redcode.Enums
{
    public static class EnumHelper
    {

        public static int GetEnumLen(Type enumType)
        {
            return Enum.GetNames(enumType).Length;
        }

    }
}
