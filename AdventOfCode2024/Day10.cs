using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024
{
    internal class Day10
    {
        internal static int Star1()
        {
            HashSet<(int, int)> positions = new();
            var lines = File.ReadAllLines("input/input10_1.txt").Select(s => s.ToCharArray().Select(ch => int.Parse(ch.ToString())).ToArray()).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (lines[i][j] == 0)
                    {
                        positions.Add((i, j));
                    }
                }
            }

            int count = 0;
            foreach (var pos in positions)
            {
                var visited = new HashSet<(int, int)>();
                Traverse(lines, pos.Item1, pos.Item2, 0, visited);
                count += visited.Count;
            }
            return count;
        }

        private static void Traverse(int[][] lines, int i, int j, int current, HashSet<(int, int)> visited)
        {
            if (i < 0 || i >= lines.Length || j < 0 || j >= lines[0].Length || lines[i][j] != current)
            {
                return;
            }
            else if (lines[i][j] == 9)
            {
                visited.Add((i, j));
            }

            Traverse(lines, i + 1, j, lines[i][j] + 1, visited);
            Traverse(lines, i - 1, j, lines[i][j] + 1, visited);
            Traverse(lines, i, j + 1, lines[i][j] + 1, visited);
            Traverse(lines, i, j - 1, lines[i][j] + 1, visited);
        }

        private static int Traverse2(int[][] lines, int i, int j, int current)
        {
            if (i < 0 || i >= lines.Length || j < 0 || j >= lines[0].Length || lines[i][j] != current)
            {
                return 0;
            }
            else if (lines[i][j] == 9)
            {
                return 1;
            }

            return
             Traverse2(lines, i + 1, j, lines[i][j] + 1) +
             Traverse2(lines, i - 1, j, lines[i][j] + 1) +
             Traverse2(lines, i, j + 1, lines[i][j] + 1) +
             Traverse2(lines, i, j - 1, lines[i][j] + 1);
        }

        internal static int Star2()
        {
            HashSet<(int, int)> positions = new();
            var lines = File.ReadAllLines("input/input10_1.txt").Select(s => s.ToCharArray().Select(ch => int.Parse(ch.ToString())).ToArray()).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[0].Length; j++)
                {
                    if (lines[i][j] == 0)
                    {
                        positions.Add((i, j));
                    }
                }
            }

            int count = 0;
            foreach (var pos in positions)
            {
                count += Traverse2(lines, pos.Item1, pos.Item2, 0);
            }
            return count;
        }
    }
}
