using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    public class Solver
    {
        Dictionary<int, double> _fish;

        public Solver()
        {
            GetInputs();
        }

        public double Solve1()
        {
            var fishCopy = new Dictionary<int, double>(_fish);
            return SimulateFish(fishCopy, 80);
        }

        public double Solve2()
        {
            var fishCopy = new Dictionary<int, double>(_fish);
            return SimulateFish(fishCopy, 256);
        }

        static double SimulateFish(Dictionary<int, double> fish, int daysToSim)
        {
            // PrintFish(fish);

            for (var i = 1; i <= daysToSim; i++)
            {
                for (var j = 0; j <= 8; j++)
                {
                    fish[j - 1] = fish[j];
                    fish[j] = 0;
                }

                // spawn
                fish[6] += fish[-1];
                fish[8] += fish[-1];
                fish[-1] = 0;
            }

            // PrintFish(fish);

            return fish.Values.Sum();
        }

        static void PrintFish(Dictionary<int, double> fish)
        {
            Console.WriteLine($"Day 8 fish: {fish[8]}");
            Console.WriteLine($"Day 7 fish: {fish[7]}");
            Console.WriteLine($"Day 6 fish: {fish[6]}");
            Console.WriteLine($"Day 5 fish: {fish[5]}");
            Console.WriteLine($"Day 4 fish: {fish[4]}");
            Console.WriteLine($"Day 3 fish: {fish[3]}");
            Console.WriteLine($"Day 2 fish: {fish[2]}");
            Console.WriteLine($"Day 1 fish: {fish[1]}");
            Console.WriteLine($"Day 0 fish: {fish[0]}");
            Console.WriteLine($"Day -1 fish: {fish[-1]}");
        }

        void GetInputs()
        {
            var starterFish = File.ReadAllText("input.txt")
                .Split(",")
                .Select(int.Parse)
                .ToList();

            Console.WriteLine($"Read {starterFish.Count} fish");

            _fish = new Dictionary<int, double>
            {
                [-1] = 0,
                [0] = 0,
                [1] = 0,
                [2] = 0,
                [3] = 0,
                [4] = 0,
                [5] = 0,
                [6] = 0,
                [7] = 0,
                [8] = 0
            };

            foreach (var fish in starterFish)
            {
                _fish[fish]++;
            }
        }
    }
}