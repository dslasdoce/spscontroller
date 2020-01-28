using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ImpinjWrite.Models
{
    public static class Utility
    {
        /// <summary>
        /// Same function with substring
        /// With addition of output of last index of substring
        /// </summary>
        public static string Slice(this string source, ref int index, int length)
        {
            if (source.Length < index + length)
                source = source.Substring(index);
            else
                source = source.Substring(index, length);

            index += length;

            // Return Substring of length
            return source;
        }

        public static bool MatchRegex(this string value, string pattern)
        {
            var regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        public static bool IsHex(this string value)
        {
            var regex = new Regex("^[0-9A-Fa-f]+$");
            return regex.IsMatch(value);
        }


    }
}
