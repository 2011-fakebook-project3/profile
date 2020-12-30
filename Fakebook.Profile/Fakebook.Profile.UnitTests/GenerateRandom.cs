using System;

namespace Fakebook.Profile.UnitTests
{
    public static class GenerateRandom
    {
        public static Random s_random = new Random();

        public static int GenerateInRange(int min, int max)
        {
            return s_random.Next(max + 1 - min) + min;
        }

        public static string PhoneNumber()
        {
            int[] ints = new int[10];

            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = GenerateInRange(0, 9);
            }

            int c = 0;
            return $"({ints[c++]}{ints[c++]}{ints[c++]}) {ints[c++]}{ints[c++]}{ints[c++]}-{ints[c++]}{ints[c++]}{ints[c++]}{ints[c++]}";
        }

        public static DateTime DateTime()
        {
            int month = GenerateInRange(1, 12);
            int day = GenerateInRange(1, 28);
            int year = GenerateInRange(1980, 2010);

            return new DateTime(year, month, day);
        }

        public static string String()
        {
            int length = GenerateInRange(8, 16);
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
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

            return new string(chars);
        }

        public static string Email()
        {
            string user = String();
            string domain = String();
            string end = String();

            return $"{user}@{domain}.{end}";
        }

        public static int Int(int min = 0, int max = 100)
        {
            return s_random.Next(min, max + 1);
        }

        public static double Double(double min = 0.0, double max = 100.0)
        {
            double temp = (s_random.NextDouble() * max) - min;
            double randomDoubleValue = Math.Round(temp * 1000) / 1000;
            return randomDoubleValue;
        }
    }
}
