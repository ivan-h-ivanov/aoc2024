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
    internal class Day14
    {
        internal static long Star1()
        {
            var robots = Parse();
            var height = 103;
            var width = 101;

            long q1 = 0, q2 = 0, q3 = 0, q4 = 0;
            var moved = robots.Select(r => Move(r, 100, height, width)).Select(r => (r.Item1, r.Item2)).ToList();
            foreach (var mr in moved)
            {
                if (mr.Item1 < width / 2)
                {
                    if (mr.Item2 < height / 2)
                    {
                        q1++;
                    }
                    else if (mr.Item2 > height / 2)
                    {
                        q4++;
                    }
                }
                else if (mr.Item1 > width / 2)
                {
                    if (mr.Item2 < height / 2)
                    {
                        q2++;
                    }
                    else if (mr.Item2 > height / 2)
                    {
                        q3++;
                    }
                }
            }
            return q1 * q2 * q3 * q4;
        }

        private static List<(int, int, int, int)> Parse()
        {
            List<(int, int, int, int)> list = new();
            var lines = File.ReadAllLines("input/input14_1.txt");
            var pattern = @"p=(\d+),(\d+) v=(-?\d+),(-?\d+)";
            foreach (var line in lines)
            {
                var match = Regex.Match(line, pattern);
                var p1 = int.Parse(match.Groups[1].Value);
                var p2 = int.Parse(match.Groups[2].Value);
                var v1 = int.Parse(match.Groups[3].Value);
                var v2 = int.Parse(match.Groups[4].Value);
                list.Add((p1, p2, v1, v2));
            }
            return list;
        }

        private static (int, int, int, int) Move((int, int, int, int) robot, int times, int height, int width)
        {
            if (times == 0)
            {
                return robot;
            }
            var moveX = (robot.Item1 + robot.Item3) % width;
            var moveY = (robot.Item2 + robot.Item4) % height;

            moveX = moveX < 0 ? width + moveX : moveX;
            moveY = moveY < 0 ? height + moveY : moveY;

            return Move((moveX, moveY, robot.Item3, robot.Item4), times - 1, height, width);
        }

        internal static long Star2()
        {
            var robots = Parse();
            var height = 103;
            var width = 101;

            for (int i = 1; i < 10000; i++)
            {
                robots = robots.Select(r => Move(r, 1, height, width)).ToList();
                Bitmap bitmap = new Bitmap(width, height);
                for (int h = 0; h < height; h++)
                {
                    for (int w = 0; w < width; w++)
                    {
                        bitmap.SetPixel(w, h, Color.White);
                    }
                }
                for (int r = 0; r < robots.Count; r++)
                {
                    bitmap.SetPixel(robots[r].Item1, robots[r].Item2, Color.Black);
                }
                string filePath = $"output{i}.png";
                bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            // See output6876.png :D. Merry X-MAS!
            return 6876;
        }
    }
}