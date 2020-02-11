namespace Panelak.Utils
{
    using System;

    /// <summary>
    /// Service extensions
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Tries to parse a string to a nullable enum value. If parsing fails,
        /// returns the null value instead.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String to parse</param>
        /// <param name="result">Nullable enum</param>
        /// <returns>True if parsing succeeded, false otherwise.</returns>
        public static bool TryParseNullableEnum<T>(this string value, out T? result) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new Exception("This method is only for Enums");

            if (Enum.TryParse(value, out T tempResult))
            {
                result = tempResult;
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Attempts to parse the nullable enum. If the parsing fails, returns null instead.
        /// </summary>
        /// <typeparam name="T">Type of the enum</typeparam>
        /// <param name="value">String to parse</param>
        /// <returns>Enum if parsing succeeds, null otherwise.</returns>
        public static T? ParseNullableEnum<T>(this string value) where T : struct, IConvertible
        {
            if (Enum.TryParse(value, out T tempResult))
                return tempResult;

            return null;
        }
    }
}
