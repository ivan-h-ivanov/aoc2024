using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal static class Day2
    {
        internal static int Star1()
        {
            var lines = File.ReadAllLines("input/input2_1.txt");
            string[] line = null;
            int next = 0, current = 0;
            bool ascending = false;
            int safeCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split();
                ++safeCount;

                for (int j = 0; j < line.Length - 1; j++)
                {
                    if (j == 0)
                    {
                        current = int.Parse(line[0]);
                        next = int.Parse(line[1]);
                        ascending = current < next;
                    }
                    else
                    {
                        current = next;
                        next = int.Parse(line[j + 1]);
                    }

                    var diff = ascending ? next - current : current - next;
                    if (diff < 1 || diff > 3)
                    {
                        --safeCount;
                        break;
                    }
                }
            }

            return safeCount;
        }

        internal static int Star2()
        {
            var lines = File.ReadAllLines("input/input2_1.txt");
            int[] line = null;
            int safeCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                line = lines[i].Split().Select(i => int.Parse(i)).ToArray();
                int[] modLine = new int[line.Length - 1];

                for (int j = 0; j < line.Length; j++)
                {
                    modLine = line.Take(j).Concat(line.Skip(j + 1)).ToArray();
                    if (IsSafe(modLine))
                    {
                        ++safeCount;
                        break;
                    }
                }
            }

            return safeCount;
        }

        private static bool IsSafe(int[] line)
        {
            int next = 0, current = 0;
            bool ascending = false;

            for (int j = 0; j < line.Length - 1; j++)
            {
                if (j == 0)
                {
                    current = line[0];
                    next = line[1];
                    ascending = current < next;
                }
                else
                {
                    current = next;
                    next = line[j + 1];
                }

                var diff = ascending ? next - current : current - next;
                if (diff < 1 || diff > 3)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
