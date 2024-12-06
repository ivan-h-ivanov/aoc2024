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
    internal static class Day6
    {
        static Dictionary<char, Func<(int, int), (int, int)>> dir = new Dictionary<char, Func<(int, int), (int, int)>>()
        {
            { '>', (x) => (x.Item1 + 1, x.Item2) },
            { '<', (x) => (x.Item1 - 1, x.Item2) },
            { '^', (x) => (x.Item1, x.Item2 - 1) },
            { 'v', (x) => (x.Item1, x.Item2 + 1) },
        };

        static Dictionary<char, char> rot = new Dictionary<char, char>()
        {
            { '>', 'v' },
            { '<', '^' },
            { '^', '>' },
            { 'v', '<' },
        };

        internal static int Star1()
        {
            HashSet<(int, int)> visited = new HashSet<(int, int)>();

            var lines = File.ReadAllLines("input/input6_1.txt").Select(s => s.ToCharArray()).ToArray();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (dir.ContainsKey(lines[i][j]))
                    {
                        visited.Add(((j, i)));
                        while (true)
                        {
                            try
                            {
                                var (a, b) = dir[lines[i][j]]((j, i));
                                if (lines[b][a] == '#')
                                {
                                    lines[i][j] = rot[lines[i][j]];
                                }
                                else
                                {
                                    lines[b][a] = lines[i][j];
                                    lines[i][j] = '.';
                                    j = a;
                                    i = b;
                                    visited.Add(((j, i)));
                                }
                            }
                            catch (IndexOutOfRangeException)
                            {
                                return visited.Count;
                            }
                        }
                    }
                }
            }

            return 1;
        }

        internal static int Star2()
        {
            var lines = File.ReadAllLines("input/input6_1.txt");
            return 1;
        }
    }
}
