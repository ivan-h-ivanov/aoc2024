using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal static class Day4
    {
        internal static int Star1()
        {
            var map = File.ReadAllLines("input/input4_1.txt");

            int count = 0;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 'X')
                    {
                        if (y < map.Length - 3 && map[y + 1][x] == 'M' && map[y + 2][x] == 'A' && map[y + 3][x] == 'S')
                        {
                            count++;
                        }
                        if (x < map[0].Length - 3 && map[y][x + 1] == 'M' && map[y][x + 2] == 'A' && map[y][x + 3] == 'S')
                        {
                            count++;
                        }
                        if (y > 2 && map[y - 1][x] == 'M' && map[y - 2][x] == 'A' && map[y - 3][x] == 'S')
                        {
                            count++;
                        }
                        if (x > 2 && map[y][x - 1] == 'M' && map[y][x - 2] == 'A' && map[y][x - 3] == 'S')
                        {
                            count++;
                        }
                        if (x > 2 && y > 2 && map[y - 1][x - 1] == 'M' && map[y - 2][x - 2] == 'A' && map[y - 3][x - 3] == 'S')
                        {
                            count++;
                        }
                        if (x > 2 && y < map.Length - 3 && map[y + 1][x - 1] == 'M' && map[y + 2][x - 2] == 'A' && map[y + 3][x - 3] == 'S')
                        {
                            count++;
                        }
                        if (x < map[0].Length - 3 && y > 2 && map[y - 1][x + 1] == 'M' && map[y - 2][x + 2] == 'A' && map[y - 3][x + 3] == 'S')
                        {
                            count++;
                        }
                        if (x < map[0].Length - 3 && y < map.Length - 3 && map[y + 1][x + 1] == 'M' && map[y + 2][x + 2] == 'A' && map[y + 3][x + 3] == 'S')
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        internal static int Star2()
        {
            var map = File.ReadAllLines("input/input4_1.txt");

            int count = 0;
            for (int y = 1; y < map.Length - 1; y++)
            {
                for (int x = 1; x < map[y].Length - 1; x++)
                {
                    if (map[y][x] == 'A')
                    {
                        if (((map[y - 1][x - 1] == 'M' && map[y + 1][x + 1] == 'S') || (map[y - 1][x - 1] == 'S' && map[y + 1][x + 1] == 'M')) &&
                            ((map[y + 1][x - 1] == 'M' && map[y - 1][x + 1] == 'S') || (map[y + 1][x - 1] == 'S' && map[y - 1][x + 1] == 'M')))
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}
