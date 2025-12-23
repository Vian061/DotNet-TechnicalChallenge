using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingBlocks.Common.Extentions
{
    public static class StringExtensions
    {
        public static string Right(this string s, int length)
        {
            length = Math.Max(length, 0);
            if (s.Length > length)
            {
                return s.Substring(s.Length - length, length);
            }

            return s;
        }

        public static string Left(this string s, int length)
        {
            length = Math.Max(length, 0);
            if (s.Length > length)
            {
                return s.Substring(0, length);
            }

            return s;
        }

        public static T ToEnum<T>(this string value) where T : new()
        {
            if (!typeof(T).IsEnum)
            {
                throw new NotSupportedException("T must be an Enum");
            }

            try
            {
                return (T)Enum.Parse(typeof(T), value);
            }
            catch
            {
                return default!;
            }
        }

        public static T ToEnumOrDefault<T>(this string? value) where T : struct, Enum
        {
            return Enum.TryParse(value, out T result) ? result : default;
        }

        public static int ToInt(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return 0;
            }

            return Convert.ToInt32(text.Replace(",", "").Replace(".", ""));
        }

        public static string GenerateRandomString(this string charSet, int length)
        {
            Random random = new Random();
            if (charSet == null || charSet.Length == 0)
            {
                throw new ArgumentException("Character set must not be empty.");
            }

            if (length < 0)
            {
                throw new ArgumentException("Length must not be negative.");
            }

            return new string((from s in Enumerable.Repeat(charSet, length)
                               select s[random.Next(s.Length)]).ToArray());
        }
    }
}
