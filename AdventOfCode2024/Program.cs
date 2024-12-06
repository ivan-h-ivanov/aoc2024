using System;
using System.Linq;

namespace AdventOfCode2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input;
            Console.WriteLine("Enter date.star input");

            while ((input = Console.ReadLine()) != null)
            {
                try
                {
                    var pair = input.Split('.');
                    var day = int.Parse(pair[0]);
                    var star = int.Parse(pair[1]);
                    var res = Type.GetType($"AdventOfCode2024.Day{day}").GetMethod($"Star{star}", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic).Invoke(null, null);
                    Console.WriteLine(res);
                }
                catch
                {
                    Console.WriteLine("Format should be #D.#S, i.e. 4.2 for day 4, star 2");
                }
            }
        }
    }
}
