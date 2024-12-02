using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024
{
    internal static class Day1
    {
        private static (List<int> left, List<int> right) Parse()
        {
            var lines = File.ReadAllLines("input/input1_1.txt");

            var left = new List<int>(lines.Length);
            var right = new List<int>(lines.Length);

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split();

                left.Add(int.Parse(line[0]));
                right.Add(int.Parse(line[3]));
            }
            return (left, right);
        }

        internal static int Star1()
        {
            var (left, right) = Parse();
            return Task1(left, right);
        }

        internal static int Star2()
        {
            var (left, right) = Parse();
            return Task2(left, right);
        }

        private static int Task1(List<int> left, List<int> right)
        {
            int aggr = 0;
            left.Sort();
            right.Sort();

            for (int i = 0; i < left.Count; i++)
            {
                aggr += Math.Abs(right[i] - left[i]);
            }

            return aggr;
        }

        private static int Task2(List<int> left, List<int> right)
        {
            int aggr = 0;
            var dict = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

            for (int i = 0; i < left.Count; i++)
            {
                var val = left[i];
                var weightedVal = dict.ContainsKey(val) ? val * dict[left[i]] : 0;
                aggr += weightedVal;
            }

            return aggr;
        }
    }
}
