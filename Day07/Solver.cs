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

            var cost = CalculateCostToMoveToPosition(_histogram, medianPosition, CalculatePart1DistanceCost);

            return cost;
        }

        public int Solve2()
        {
            var costs = new List<int>();

            for (var i = _crabs.First(); i < _crabs.Last(); i++)
            {
                var cost = CalculateCostToMoveToPosition(_histogram, i, CalculatePart2DistanceCost);
                costs.Add(cost);
            }

            return costs.Min();
        }

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

        static int CalculateCostToMoveToPosition(Dictionary<int, int> histogram, int destination, Func<int,int,int> calculationStrategy)
        {
            var cost = 0;

            foreach (var key in histogram.Keys)
            {
                var distance = calculationStrategy(key, destination);

                cost += distance * histogram[key];
            }

            return cost;
        }

        static int CalculatePart1DistanceCost(int start, int end)
        {
            return Math.Abs(start - end);
        }

        static int CalculatePart2DistanceCost(int start, int end)
        {
            var distance = Math.Abs(start - end);

            return (int)((Math.Pow(distance, 2) + distance) / 2);
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