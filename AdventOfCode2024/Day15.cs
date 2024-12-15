using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Day15
    {
        static Dictionary<char, Func<(int, int), (int, int)>> move = new()
        {
            { '<', (pos) => (pos.Item1, pos.Item2 - 1) },
            { '>', (pos) => (pos.Item1, pos.Item2 + 1) },
            { '^', (pos) => (pos.Item1 - 1, pos.Item2) },
            { 'v', (pos) => (pos.Item1 + 1, pos.Item2) }
        };

        internal static long Star1()
        {
            var lines = File.ReadAllLines("input/input15_1.txt").ToList();
            var emptyLine = lines.IndexOf(string.Empty);
            var startLine = lines.First(l => l.Contains('@'));
            var map = lines.Take(emptyLine).Select(s => s.ToCharArray()).ToArray();
            var sequence = lines.Skip(emptyLine + 1).SelectMany(s => s.ToCharArray()).ToArray();

            (int, int) start = (lines.IndexOf(startLine), startLine.IndexOf('@'));
            for (int i = 0; i < sequence.Length; i++)
            {
                start = Move1(map, start, move[sequence[i]]) ? move[sequence[i]](start) : start;
            }

            var aggr = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'O')
                    {
                        aggr += 100 * i + j;
                    }
                }
            }
            return aggr;
        }

        private static bool Move1(char[][] map, (int, int) pos, Func<(int, int), (int, int)> next)
        {
            if (map[pos.Item1][pos.Item2] == '#')
            {
                return false;
            }
            if (map[pos.Item1][pos.Item2] == '.')
            {
                return true;
            }

            var nextPos = next(pos);
            bool res = Move1(map, nextPos, next);
            if (res)
            {
                map[nextPos.Item1][nextPos.Item2] = map[pos.Item1][pos.Item2];
                map[pos.Item1][pos.Item2] = '.';
            }
            return res;
        }

        internal static long Star2()
        {
            var lines = File.ReadAllLines("input/input15_1.txt").ToList();
            var emptyLine = lines.IndexOf(string.Empty);
            var startLine = lines.First(l => l.Contains('@'));
            var map = lines.Take(emptyLine).Select(s => s.Replace("#", "##").Replace(".", "..").Replace("O", "[]").Replace("@", "@.").ToCharArray()).ToArray();
            var sequence = lines.Skip(emptyLine + 1).SelectMany(s => s.ToCharArray()).ToArray();
           
            (int, int) start = (lines.IndexOf(startLine), 2 * startLine.IndexOf('@'));
            for (int i = 0; i < sequence.Length; i++)
            {
                var canMove = CanMove(map, start, sequence[i]);
                if (canMove)
                {
                    Move2(map, start, sequence[i]);
                }
                start =  canMove ? move[sequence[i]](start) : start;
            }

            var aggr = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '[')
                    {
                        aggr += 100 * i + j;
                    }
                }
            }
            return aggr;
        }

        private static bool CanMove(char[][] map, (int, int) pos, char dir)
        {
            var next = move[dir];
            if (map[pos.Item1][pos.Item2] == '#')
            {
                return false;
            }
            if (map[pos.Item1][pos.Item2] == '.')
            {
                return true;
            }

            var nextPos = next(pos);
            bool res = false;
            if (dir == '^' || dir == 'v')
            {
                if (map[nextPos.Item1][nextPos.Item2] == ']')
                {
                    res = CanMove(map, (nextPos.Item1, nextPos.Item2 - 1), dir) && CanMove(map, nextPos, dir);
                }
                else if (map[nextPos.Item1][nextPos.Item2] == '[')
                {
                    res = CanMove(map, (nextPos.Item1, nextPos.Item2 + 1), dir) && CanMove(map, nextPos, dir);
                }
                else
                {
                    res = CanMove(map, nextPos, dir);
                }
            }
            else
            {
                res = CanMove(map, nextPos, dir);
            }
            return res;
        }

        private static void Move2(char[][] map, (int, int) pos, char dir)
        {
            var next = move[dir];
            if (map[pos.Item1][pos.Item2] == '#')
            {
                return;
            }
            if (map[pos.Item1][pos.Item2] == '.')
            {
                return;
            }

            var nextPos = next(pos);
            if (dir == '^' || dir == 'v')
            {
                if (map[nextPos.Item1][nextPos.Item2] == ']')
                {
                    Move2(map, (nextPos.Item1, nextPos.Item2 - 1), dir);
                    Move2(map, nextPos, dir);
                }
                else if (map[nextPos.Item1][nextPos.Item2] == '[')
                {
                    Move2(map, (nextPos.Item1, nextPos.Item2 + 1), dir);
                    Move2(map, nextPos, dir);
                }
                else
                {
                    Move2(map, nextPos, dir);
                }
            }
            else
            {
                Move2(map, nextPos, dir);
            }
            map[nextPos.Item1][nextPos.Item2] = map[pos.Item1][pos.Item2];
            map[pos.Item1][pos.Item2] = '.';
        }
    }
}