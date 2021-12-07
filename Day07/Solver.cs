using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day07
{
    public class Solver
    {
        List<int> _crabs;
        Dictionary<int, int> _histogram;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            var medianPosition = CalculateMedian(_crabs);

            var cost = CalculateCostToMoveToPosition(_histogram, medianPosition);

            return cost;
        }

        // public double Solve2()
        // {
        //     var fishCopy = new Dictionary<int, double>(_fish);
        //     return SimulateFish(fishCopy, 256);
        // }

        static int CalculateMedian(List<int> crabs)
        {
            var count = crabs.Count;

            if (count % 2 == 0)
            {
                var temp = crabs.Skip((count / 2) - 1).Take(2).Average();
                return (int) Math.Floor(temp);
            }

            return crabs.ElementAt(count / 2);
        }

        static int CalculateCostToMoveToPosition(Dictionary<int, int> histogram, int destination)
        {
            var cost = 0;

            foreach (var key in histogram.Keys)
            {
                var distance = Math.Abs(destination - key);

                cost += distance * histogram[key];
            }

            return cost;
        }

        
        void GetInputs()
        {
            _crabs = File.ReadAllText("input.txt")
                .Split(",")
                .Select(int.Parse)
                .OrderBy(x => x)
                .ToList();

            Console.WriteLine($"Read {_crabs.Count} crabs");

            _histogram = _crabs
                .GroupBy(x => x)
                .Select(x => new {Pos = x.Key, Count = x.Count()})
                .ToDictionary(x => x.Pos, x => x.Count);
        }
    }
}