using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;

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
            var lines = File.ReadAllLines("input/input6_1.txt").Select(s => s.ToCharArray()).ToArray();
            GetStart(lines, out int i, out int j);
            return Traverse(i, j, lines, false).Count;
        }

        internal static int Star2()
        {
            var lines = File.ReadAllLines("input/input6_1.txt");

            var initial = lines.Select(s => s.ToCharArray()).ToArray();
            GetStart(initial, out int i, out int j);
            var visited = Traverse(i, j, initial, false).Skip(1).ToList();
            int count = 0;

            for (int c = 0; c < visited.Count; c++)
            {
                var lnC = lines.Select(s => s.ToCharArray()).ToArray();
                lnC[visited[c].Item2][visited[c].Item1] = '#';
                if (Traverse(i, j, lnC, true) == null)
                    count++;
            }
            return count;
        }

        private static (int, int) GetStart(char[][] lines, out int i, out int j)
        {
            i = 0; j = 0;
            for (i = 0; i < lines.Length; i++)
            {
                for (j = 0; j < lines[i].Length; j++)
                {
                    if (dir.ContainsKey(lines[i][j]))
                    {
                        return (i, j);
                    }
                }
            }
            return (i, j);
        }

        private static HashSet<(int, int, char)> Traverse(int i, int j, char[][] lines, bool breakOnRepeat)
        {
            EqualityComparer<(int, int, char)> hf = !breakOnRepeat ?
            EqualityComparer<(int, int, char)>.Create((x, y) => x.Item1 == y.Item1 && y.Item2 == x.Item2, x => (x.Item1, x.Item2).GetHashCode()) :
            EqualityComparer<(int, int, char)>.Create((x, y) => x.Item1 == y.Item1 && y.Item2 == x.Item2 && x.Item3 == y.Item3, x => x.GetHashCode());

            HashSet<(int, int, char)> visited = new HashSet<(int, int, char)>(hf);

            if (dir.ContainsKey(lines[i][j]))
            {
                visited.Add((j, i, lines[i][j]));
                while (true)
                {
                    var (a, b) = dir[lines[i][j]]((j, i));

                    if (a < 0 || b < 0 || a >= lines[0].Length || b >= lines.Length)
                    {
                        return visited;
                    }

                    if (lines[b][a] == '#')
                    {
                        lines[i][j] = rot[lines[i][j]];
                    }
                    else
                    {
                        lines[b][a] = lines[i][j];
                        lines[i][j] = '.';
                        j = a; i = b;
                        if (!visited.Add((j, i, lines[i][j])) && breakOnRepeat)
                        {
                            return null;
                        }
                    }
                }
            }
            return visited;
        }
    }
}
