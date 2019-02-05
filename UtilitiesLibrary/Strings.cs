using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilitiesLibrary
{
    public static class Strings
    {
        public static List<string> ToStringList(this string value, char separator) => value.Split(separator).OfType<string>().ToList();
        public static string StripCharacter(this string variable, string characters) => variable.Replace(characters, string.Empty);
        public static string ToSnakeCase(this string str) => str.ToLower().Replace(' ', '_');
        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            maxLength = Math.Abs(maxLength);
            return (value.Length <= maxLength ? value : value.Substring(0, maxLength));
        }
    }
}
