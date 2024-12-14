using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024
{
    internal class Day11
    {
        static Dictionary<(long, int), long> memo;

        internal static long Star1()
        {
            memo = new();
            var line = File.ReadAllLines("input/input11_1.txt")[0].Split(' ').Select(int.Parse).ToArray();
            return line.Select(s => Split(s, 25)).Sum();
        }

        internal static long Star2()
        {
            memo = new();
            var line = File.ReadAllLines("input/input11_1.txt")[0].Split(' ').Select(int.Parse).ToArray();
            return line.Select(s => Split(s, 75)).Sum();
        }

        private static long Split(long num, int count)
        {
            long res = 0;
            if (count == 0)
            {
                return 1;
            }
            if (memo.ContainsKey((num, count)))
            {
                return memo[(num, count)];
            }
            var log = (int)Math.Log10(num) + 1;
            if (num == 0)
            {
                res += Split(1, count - 1);
            }
            else if (log % 2 == 0)
            {
                log = log / 2;

                var num1 = num / (int)Math.Pow(10, log);
                var num2 = num % (int)Math.Pow(10, log);
                res += Split(num1, count - 1) + Split(num2, count - 1);
            }
            else
            {
                res += Split(num * 2024, count - 1);
            }

            memo[(num, count)] = res;
            return res;
        }
    }
}
