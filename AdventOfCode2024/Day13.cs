using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Day13
    {
        internal static long Star1()
        {
            var lines = File.ReadAllLines("input/input13_1.txt");
            return Parse(lines, false).Select(mtr => Solve(mtr.Item1, mtr.Item2, false)).Sum();
        }

        internal static long Star2()
        {
            var lines = File.ReadAllLines("input/input13_1.txt");
            return Parse(lines, true).Select(mtr => Solve(mtr.Item1, mtr.Item2, true)).Sum();
        }

        private static List<(long[,], long[,])> Parse(string[] lines, bool isLong)
        {
            var capture = @"(\d+).*?(\d+)";
            List<(long[,], long[,])> matrices = new();

            for (long i = 0; i < lines.Length; i += 4)
            {
                long[,] m = new long[2, 2];
                long[,] r = new long[2, 1];

                var match = Regex.Match(lines[i], capture);
                if (match.Success)
                {
                    long x = long.Parse(match.Groups[1].Value);
                    long y = long.Parse(match.Groups[2].Value);

                    m[0, 0] = x;
                    m[1, 0] = y;
                }

                match = Regex.Match(lines[i + 1], capture);
                if (match.Success)
                {
                    long x = long.Parse(match.Groups[1].Value);
                    long y = long.Parse(match.Groups[2].Value);

                    m[0, 1] = x;
                    m[1, 1] = y;
                }

                match = Regex.Match(lines[i + 2], capture);
                if (match.Success)
                {
                    long x = long.Parse(match.Groups[1].Value);
                    long y = long.Parse(match.Groups[2].Value);

                    r[0, 0] = isLong ? 10000000000000 + x : x;
                    r[1, 0] = isLong ? 10000000000000 + y : y;
                }

                matrices.Add((m, r));
            }
            return matrices;
        }

        private static long Solve(long[,] var, long[,] res, bool isLong)
        {
            double det = var[0, 0] * var[1, 1] - var[0, 1] * var[1, 0];
            double[,] rev = new double[,]
            {
                { var[1, 1] / det, -var[0, 1] / det },
                { -var[1, 0] / det, var[0, 0] / det }
            };

            double[,] result = new double[,]
            {
                { rev[0, 0] * res[0, 0] + rev[0, 1] * res[1, 0] },
                { rev[1, 0] * res[0, 0] + rev[1, 1] * res[1, 0] }
            };

            var x = Math.Round(result[0, 0], 3);
            var y = Math.Round(result[1, 0], 3);

            if (x < 0 || y < 0)
            {
                return 0;
            }

            if (!isLong && (x > 100 || y > 100))
            {
                return 0;
            }

            if (Math.Abs(x % 1) < 1e-10 && Math.Abs(y % 1) < 1e-10)
            {
                return 3 * (long)x + (long)y;
            }
            else
            {
                return 0;
            }
        }
    }
}