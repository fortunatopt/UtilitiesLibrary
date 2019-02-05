using System;
using System.Collections.Generic;
using System.Text;

namespace UtilitiesLibrary
{
    public static class Numbers
    {
        /// <summary>
        /// Method for converting a string of integers into an array of longs
        /// </summary>
        /// <param name="value">String of Value to be parsed</param>
        /// <param name="separator">Character of Separator Character</param>
        /// <returns>Array of Long</returns>
        public static long[] ToLongArray(this string value, char separator)
        {
            value = value.Replace(" ", null);
            string[] array = null;

            try
            {
                array = value.Split(separator);
            }
            catch
            {
                throw;
            }

            long[] longArray = null;

            try
            {
                longArray = Array.ConvertAll(array, s => long.Parse(s));
            }
            catch
            {
                throw;
            }

            return longArray;

        }

        /// <summary>
        /// Method for converting a string of integers into an array of integers
        /// </summary>
        /// <param name="value">String of Value to be parsed</param>
        /// <param name="separator">Character of Separator Character</param>
        /// <returns>Array of Integers</returns>
        public static int[] ToIntArray(this string value, char separator) => Array.ConvertAll(value.Split(separator), s => int.Parse(s));

        /// <summary>
        /// Method for converting a string to an int
        /// </summary>
        /// <param name="input">String of Input to be converted</param>
        /// <returns>Integer</returns>
        public static int StringToInt(this string input)
        {
            int output = int.TryParse(input, out output) ? output : 0;

            return output;
        }
    }
}
