using System;
using System.Linq;

namespace AdventOfCode2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string res;
            Console.WriteLine("Enter date.star input");
            while ((res = Console.ReadLine()) != null)
            {
                switch (res)
                {
                    case "1.1":
                        Console.WriteLine(Day1.Star1());
                        break;
                    case "1.2":
                        Console.WriteLine(Day1.Star2());
                        break;
                    case "2.1":
                        Console.WriteLine(Day2.Star1());
                        break;
                    case "2.2":
                        Console.WriteLine(Day2.Star2());
                        break;
                    default:
                        Console.WriteLine("Format should be #D.#S, i.e. 4.2 for day 4, star 2");
                        break;
                }
            }
        }
    }
}
