using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal static class Day5
    {
        internal static int Star1()
        {
            return Aggregate(Calc1);
        }

        internal static int Star2()
        {
            return Aggregate(Calc2);
        }

        private static int Aggregate(Func<HashSet<(string, string)>, string, int> calc)
        {
            HashSet<(string, string)> charSet = new HashSet<(string, string)>();
            var lines = File.ReadAllLines("input/input5_1.txt");
            int i;
            for (i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line == string.Empty)
                {
                    i++;
                    break;
                }
                var split = line.Split('|');
                charSet.Add((split[1], split[0]));
            }

            int aggr = 0;
            for (; i < lines.Length; i++)
            {
                aggr += calc(charSet, lines[i]);
            }
            return aggr;
        }

        private static int Calc1(HashSet<(string, string)> charSet, string line)
        {
            var split = line.Split(',');
            for (int j = 0; j < split.Length; j++)
            {
                for (int k = j + 1; k < split.Length; k++)
                {
                    if (charSet.Contains((split[j], split[k])))
                    {
                        return 0;
                    }
                }
            }
            return int.Parse(split[split.Length / 2]);
        }

        private static int Calc2(HashSet<(string, string)> charSet, string line)
        {
            var split = line.Split(',');
            bool swapped = false;

            for (int j = 0; j < split.Length; j++)
            {
                for (int k = j + 1; k < split.Length; k++)
                {
                    if (charSet.Contains((split[j], split[k])))
                    {
                        var t = split[j];
                        split[j] = split[k];
                        split[k] = t;
                        swapped = true;
                    }
                }
            }
            return swapped ? int.Parse(split[split.Length / 2]) : 0;
        }
    }
}
