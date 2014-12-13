using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkParser
{
    public static class StringExtension
    {
        public static IEnumerable<int> AllIndexesOf(this string str, string value)
        {
            if (string.IsNullOrEmpty(str))
                yield break;

            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");

            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index, StringComparison.Ordinal);
                if (index == -1)
                    break;
                yield return index;
            }
        }
    }

    public static class EnumerableExtensions
    {
        private static readonly Random Rnd = new Random();

        public static T Random<T>(this IEnumerable<T> input)
        {
            var enumerable = input as IList<T> ?? input.ToList();
            return enumerable.ElementAt(Rnd.Next(enumerable.Count()));
        }
    }
}
