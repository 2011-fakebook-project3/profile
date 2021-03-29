using System;

namespace Fakebook.Profile.UnitTests.TestData
{
    public static class GenerateRandom
    {
        public static Random s_random = new();

        public static int GenerateInRange(int min, int max)
        {
            return s_random.Next(max + 1 - min) + min;
        }

        /// <summary>
        /// return a randomized phone number in the correct format
        /// </summary>
        /// <returns></returns>
        public static string PhoneNumber()
        {
            var ints = new int[10];

            for (var i = 0; i < ints.Length; i++)
            {
                ints[i] = GenerateInRange(0, 9);
            }

            var c = 0;
            return $"({ints[c++]}{ints[c++]}{ints[c++]}) {ints[c++]}{ints[c++]}{ints[c++]}-{ints[c++]}{ints[c++]}{ints[c++]}{ints[c++]}";
        }

        /// <summary>
        /// return a randomized date in the correct format
        /// </summary>
        /// <returns></returns>
        public static DateTime DateTime()
        {
            int month = GenerateInRange(1, 12);
            int day = GenerateInRange(1, 28);
            int year = GenerateInRange(1880, 2020);

            return new(year, month, day);
        }

        /// <summary>
        /// return a randomized string for first name and last name in the correct format
        /// </summary>
        /// <returns></returns>
        public static string String()
        {
            int length = GenerateInRange(8, 16);
            var chars = new char[length];

            for (var i = 0; i < length; i++)
            {
                int min, max;
                bool uppercase = s_random.Next(2) == 1;
                if (uppercase)
                {
                    min = 65;
                    max = 90;
                }
                else
                {
                    min = 97;
                    max = 122;
                }
                int temp = GenerateInRange(min, max);
                chars[i] = (char)temp;
            }
            return new(chars);
        }

        /// <summary>
        /// return a randomized user email in the correct format
        /// </summary>
        /// <returns></returns>
        public static string Email()
        {
            string user = String();
            string domain = String();
            string end = String();
            return $"{user}@{domain}.{end}";
        }

        /// <summary>
        /// helper method to generate a number between 0-100
        /// </summary>
        /// <returns></returns>
        public static int Int(int min = 0, int max = 100)
        {
            return s_random.Next(min, max + 1);
        }

        /// <summary>
        /// helper method to generate a double between 0-100
        /// </summary>
        /// <returns></returns>
        public static double Double(double min = 0.0, double max = 100.0)
        {
            double temp = (s_random.NextDouble() * max) - min;
            double randomDoubleValue = Math.Round(temp * 1000) / 1000;
            return randomDoubleValue;
        }
    }
}
