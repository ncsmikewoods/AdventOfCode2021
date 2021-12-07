using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    public class Solver
    {
        Dictionary<int, int> _fish;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            var daysToSim = 80;

            for (var i = 1; i <= daysToSim; i++)
            {
                for (var j = 0; j <= 8; j++)
                {
                    _fish[j - 1] = _fish[j];
                    _fish[j] = 0;
                }

                // spawn
                _fish[6] += _fish[-1];
                _fish[8] += _fish[-1];
                _fish[-1] = 0;
            }

            return _fish.Values.Sum();
        }

        // public int Solve2()
        // {
        //     return 0;
        // }

        void GetInputs()
        {
            var starterFish = File.ReadAllText("input.txt")
                .Split(",")
                .Select(int.Parse)
                .ToList();
            Console.WriteLine($"Read {starterFish.Count} fish");

            _fish = new Dictionary<int, int>
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