using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            }

            return aggr;
        }

        internal static long Star2()
        {
            var line = File.ReadAllLines("input/input9_1.txt")[0];
            var ln = line.Select(s => int.Parse(s.ToString())).ToArray();
            List<(int, int)> full = new();
            Parse(ln, full);
            Solve(full, full.Count - 1);
            var defrag = full.Where(full => full.Item2 != 0).ToList();

            long aggr = 0;
            for (int i = 0, j = 0; i < defrag.Count; i++)
            {
                var end = defrag[i].Item2;
                while (end-- > 0)
                {
                    aggr += j++ * (defrag[i].Item1 != -1 ? defrag[i].Item1 : 0);
                }
            }

            return aggr;
        }

        private static void Solve(List<(int, int)> full, int counter)
        {
            int i = counter;

            if (counter <= 0)
            {
                return;
            }
            for (; i >= 0; i--)
            {
                var f = full[i];
                if (f.Item1 != -1)
                {
                    for (int j = 0; j < full.Count; j++)
                    {
                        var e = full[j];
                        if (e.Item1 == -1 && j < i)
                        {
                            if (e.Item2 >= f.Item2)
                            {
                                if (e.Item2 == f.Item2)
                                {
                                    full[j] = (-1, 0);
                                }
                                else
                                {
                                    full[j] = (e.Item1, e.Item2 - f.Item2);
                                }
                                full[i] = (-1, f.Item2);
                                full.Insert(j, f);
                                Solve(full, i);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private static void Parse(int[] line, List<(int, int)> full)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (i % 2 == 0)
                {
                    full.Add((i / 2, (line[i])));
                }
                else
                {
                    full.Add((-1, (line[i])));
                }
            }
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
    }
}
