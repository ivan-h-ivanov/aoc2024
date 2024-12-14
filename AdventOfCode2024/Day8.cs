using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024
{
    internal static class Day8
    {
        internal static long Star1()
        {
            return Calc(false);
        }

        internal static long Star2()
        {
            return Calc(true);
        }
        private static int Calc(bool longSignal)
        {
            var lines = File.ReadAllLines("input/input8_1.txt");

            HashSet<(int, int)> nodes = new();
            Dictionary<char, List<(int, int)>> map = new();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var val = lines[i][j];
                    if (val != '.')
                    {
                        if (!map.ContainsKey(val))
                        {
                            map[val] = new List<(int, int)>();
                        }
                        map[val].Add((i, j));
                    }
                }
            }

            foreach (var key in map.Keys)
            {
                var values = map[key].ToList();
                for (int i = 0; i < values.Count; i++)
                {
                    if (values.Count > 1 && longSignal)
                    {
                        nodes.Add(values[i]);
                    }
                    for (int j = i + 1; j < values.Count; j++)
                    {
                        var first = values[j];
                        var second = values[i];

                        Traverse(nodes, first, second, lines.Length, longSignal ? int.MaxValue : 1);
                        Traverse(nodes, second, first, lines.Length, longSignal ? int.MaxValue : 1);
                    }
                }
            }

            return nodes.Count;
        }

        private static void Traverse(HashSet<(int, int)> nodes, (int, int) first, (int, int) second, int max, int limit)
        {
            var a1 = first.Item1 + first.Item1 - second.Item1;
            var a2 = first.Item2 + first.Item2 - second.Item2;

            while (a1 >= 0 && a1 < max && a2 >= 0 && a2 < max && limit-- > 0)
            {
                nodes.Add((a1, a2));
                second = first;
                first = (a1, a2);

                a1 = first.Item1 + first.Item1 - second.Item1;
                a2 = first.Item2 + first.Item2 - second.Item2;
            }
        }
    }
}
