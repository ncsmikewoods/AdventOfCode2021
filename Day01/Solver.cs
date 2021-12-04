using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Solver
    {
        List<int> _depths;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            var count = 0;

            for (var i = 1; i <= _depths.Count-1; i++)
            {
                if (_depths[i] > _depths[i - 1]) count++;
            }

            return count;
        }

        public int Solve2()
        {
            var count = 0;
            for (var i = 3; i <= _depths.Count - 1; i++)
            {
                if (WindowSumAt(i) > WindowSumAt(i - 1)) count++;
            }

            return count;
        }

        int WindowSumAt(int index)
        {
            return _depths[index] + _depths[index-1] + _depths[index-2];
        }

        void GetInputs()
        {
            var lines = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {lines.Length} inputs");

            _depths = lines.Select(int.Parse).ToList();
        }
    }
}