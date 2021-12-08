using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day08
{
    public class Solver
    {
        List<InputRecord> _inputRecords;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            return _inputRecords
                .Select(x => x.CountSimpleOutputs())
                .Sum();
        }

        public int Solve2()
        {
            return _inputRecords
                .Select(x => x.DescrambleOutputs())
                .Sum();
        }

        void GetInputs()
        {
            var lines = File.ReadAllLines("input.txt").ToList();
            Console.WriteLine($"Read {lines.Count} lines");

            _inputRecords = lines.Select(line => new InputRecord(line)).ToList();
        }
    }
}