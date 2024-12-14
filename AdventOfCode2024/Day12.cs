using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Day12
    {
        private static HashSet<(int, int)> visited = new();

        internal static long Star1()
        {
            visited.Clear();
            var lines = File.ReadAllLines("input/input12_1.txt");

            int count = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (visited.Contains((i, j)))
                    {
                        continue;
                    }
                    int area = 0, per = 0;
                    Traverse(lines, i, j, lines[i][j], ref per, ref area);
                    count += per * area;
                }
            }
            return count;
        }

        private static void Traverse(string[] lines, int i, int j, char ch, ref int per, ref int area)
        {
            if (i < 0 || i >= lines.Length || j < 0 || j >= lines[0].Length || lines[i][j] != ch || !visited.Add((i, j)))
            {
                return;
            }

            var basePer = 4;
            if (visited.Contains((i, j + 1)) && lines[i][j + 1] == ch)
            {
                basePer -= 2;
            }
            if (visited.Contains((i, j - 1)) && lines[i][j - 1] == ch)
            {
                basePer -= 2;
            }
            if (visited.Contains((i + 1, j)) && lines[i + 1][j] == ch)
            {
                basePer -= 2;
            }
            if (visited.Contains((i - 1, j)) && lines[i - 1][j] == ch)
            {
                basePer -= 2;
            }

            area++;
            per += basePer;

            Traverse(lines, i + 1, j, ch, ref per, ref area);
            Traverse(lines, i - 1, j, ch, ref per, ref area);
            Traverse(lines, i, j + 1, ch, ref per, ref area);
            Traverse(lines, i, j - 1, ch, ref per, ref area);
        }
        internal static long Star2()
        {
            visited.Clear();
            var lines = File.ReadAllLines("input/input12_1.txt");

            int count = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (visited.Contains((i, j)))
                    {
                        continue;
                    }
                    int area = 0;
                    (int, int) start = (i, j);
                    (int, int) end = (i, j);
                    HashSet<(int, int)> v = new();
                    Traverse2(lines, i, j, lines[i][j], ref area, ref start, ref end, v);
                    var sides = GetSides(lines, start, end, lines[i][j], v);
                    count += sides * area;
                }
            }
            return count;
        }

        private static int GetSides(string[] lines, (int, int) start, (int, int) end, char ch, HashSet<(int, int)> vis)
        {
            HashSet<(int, int, bool)> vertSides = new HashSet<(int, int, bool)>();
            for (int i = start.Item1; i <= end.Item1; i++)
            {
                bool prev = false;
                for (int j = start.Item2; j <= end.Item2; j++)
                {
                    if (lines[i][j] == ch && !vis.Contains((i, j)))
                    {
                        continue;
                    }
                    if (prev ^ (lines[i][j] == ch && vis.Contains((i, j))))
                    {
                        vertSides.Remove((i - 1, j, prev));
                        vertSides.Add((i, j, prev));
                    }
                    prev = lines[i][j] == ch;
                    if (prev && j == end.Item2 && vis.Contains((i, j)))
                    {
                        vertSides.Remove((i - 1, j + 1, prev));
                        vertSides.Add((i, j + 1, prev));
                    }
                }
            }

            HashSet<(int, int, bool)> hrzSides = new HashSet<(int, int, bool)>();
            for (int j = start.Item2; j <= end.Item2; j++)
            {
                bool prev = false;
                for (int i = start.Item1; i <= end.Item1; i++)
                {
                    if (lines[i][j] == ch && !vis.Contains((i, j)))
                    {
                        continue;
                    }
                    if (prev ^ (lines[i][j] == ch && vis.Contains((i, j))))
                    {
                        hrzSides.Remove((i, j - 1, prev));
                        hrzSides.Add((i, j, prev));
                    }
                    prev = lines[i][j] == ch;
                    if (i == end.Item1 && prev && vis.Contains((i, j)))
                    {
                        hrzSides.Remove((i + 1, j - 1, prev));
                        hrzSides.Add((i + 1, j, prev));
                    }
                }
            }
            return hrzSides.Count + vertSides.Count;
        }

        private static void Traverse2(string[] lines, int i, int j, char ch, ref int area, ref (int, int) start, ref (int, int) end, HashSet<(int, int)> v)
        {
            if (i < 0 || i >= lines.Length || j < 0 || j >= lines[0].Length || lines[i][j] != ch || !visited.Add((i, j)))
            {
                return;
            }

            if (i < start.Item1)
            {
                start.Item1 = i;
            }
            if (i > end.Item1)
            {
                end.Item1 = i;
            }
            if (j < start.Item2)
            {
                start.Item2 = j;
            }
            if (j > end.Item2)
            {
                end.Item2 = j;
            }
            area++;
            v.Add((i, j));
            Traverse2(lines, i + 1, j, ch, ref area, ref start, ref end, v);
            Traverse2(lines, i - 1, j, ch, ref area, ref start, ref end, v);
            Traverse2(lines, i, j + 1, ch, ref area, ref start, ref end, v);
            Traverse2(lines, i, j - 1, ch, ref area, ref start, ref end, v);
        }
    }
}
