using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024
{
    internal static class Day7
    {
        private static Dictionary<char, Func<long, long, long>> op = new Dictionary<char, Func<long, long, long>>()
        {
            { '+', (a, b) => a + b },
            { '*', (a, b) => a * b },
            { '|', (a, b) => a * (long)Math.Pow(10, (int)Math.Log10(b) + 1) + b }
        };

        internal static long Star1()
        {
            return Calc('+', '*');
        }

        internal static long Star2()
        {
            return Calc('+', '*', '|');
        }

        private static long Calc(params char[] operators)
        {
            var lines = File.ReadAllLines("input/input7_1.txt");
            long aggr = 0;

            for (long i = 0; i < lines.Length; i++)
            {
                var a = lines[i].Split(' ');

                var values = a.Skip(1).Select(t => long.Parse(t)).ToArray();
                var result = long.Parse(a[0].Substring(0, a[0].Length - 1));

                if (Rec(values, string.Empty, 0, result, operators))
                {
                    aggr += result;
                }
            }
            return aggr;
        }

        private static bool Rec(long[] nums, string operators, long counter, long res, params char[] opr)
        {
            if (counter == nums.Length - 1)
            {
                long aggr = nums[0];
                for (int i = 0; i < operators.Length; i++)
                {
                    aggr = op[operators[i]](aggr, nums[i + 1]);
                }
                return aggr == res;
            }

            return opr.Select(o => Rec(nums, operators + o, counter + 1, res, opr)).Any(s => s);
        }
    }
}
