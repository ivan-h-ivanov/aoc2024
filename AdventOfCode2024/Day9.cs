using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2024
{
    internal static class Day9
    {
        internal static long Star1()
        {
            var line = File.ReadAllLines("input/input9_1.txt")[0].Select(s => int.Parse(s.ToString())).ToArray();
            var defrag = Defrag(line);
            long aggr = 0;
            for (int i = 0, j = 0; i < defrag.Count; i++)
            {
                var end = defrag[i].Item2;
                while (end-- > 0)
                {
                    aggr += j++ * defrag[i].Item1;
                }
                Console.WriteLine(defrag[i]);
            }

            return aggr;
        }

        internal static long Star2()
        {
            var line = File.ReadAllLines("input/input9_1.txt")[0].Select(s => int.Parse(s.ToString())).ToArray();
            var defrag = Defrag(line);
            long aggr = 0;
            for (int i = 0, j = 0; i < defrag.Count; i++)
            {
                var end = defrag[i].Item2;
                while (end-- > 0)
                {
                    aggr += j++ * defrag[i].Item1;
                }
                Console.WriteLine(defrag[i]);
            }

            return aggr;
        }

        private static List<(int, int)> Defrag(int[] line)
        {
            List<(int, int)> defrag = new();
            var max = line.Length / 2;
            for (int i = 0, k = 0; i < line.Length; i++)
            {
                if (line.Length - 2 * k - 1 == 2 * i)
                {
                    if (defrag[defrag.Count - 1].Item1 == i)
                    {
                        var dfl = defrag[defrag.Count - 1];
                        defrag[defrag.Count - 1] = (dfl.Item1, dfl.Item2 + line[i * 2]);
                    }
                    return defrag;
                }
                defrag.Add((i, line[i * 2]));
                var empty = line[i * 2 + 1];
                int buff = empty;
                while (true && empty > 0)
                {
                    var last = line[line.Length - 1 - 2 * k];
                    buff -= last;
                    if (buff <= 0)
                    {
                        defrag.Add((max - k, last + buff));
                        line[line.Length - 1 - 2 * k] -= last + buff;
                        if (buff == 0)
                        {
                            k++;
                        }
                        break;
                    }
                    else
                    {
                        defrag.Add((max - k, last));
                        line[i + 1] = buff;
                        line[line.Length - 1 - 2 * k] = 0;
                        k++;
                    }
                }
            }
            return defrag;
        }

        private static List<(int, int)> Defrag2(int[] line)
        {
            List<(int, int)> defrag = new();
            var max = line.Length / 2;
            for (int i = 0, k = 0; i < line.Length; i++)
            {
                if (line.Length - 2 * k - 1 == 2 * i)
                {
                    if (defrag[defrag.Count - 1].Item1 == i)
                    {
                        var dfl = defrag[defrag.Count - 1];
                        defrag[defrag.Count - 1] = (dfl.Item1, dfl.Item2 + line[i * 2]);
                    }
                    return defrag;
                }
                defrag.Add((i, line[i * 2]));
                var empty = line[i * 2 + 1];
                int buff = empty;
                while (true && empty > 0)
                {
                    var last = line[line.Length - 1 - 2 * k];
                    buff -= last;
                    if (buff <= 0)
                    {
                        defrag.Add((max - k, last + buff));
                        line[line.Length - 1 - 2 * k] -= last + buff;
                        if (buff == 0)
                        {
                            k++;
                        }
                        break;
                    }
                    else
                    {
                        defrag.Add((max - k, last));
                        line[i + 1] = buff;
                        line[line.Length - 1 - 2 * k] = 0;
                        k++;
                    }
                }
            }
            return defrag;
        }
    }
}
