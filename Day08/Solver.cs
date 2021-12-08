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

        // public int Solve2()
        // {
        //     
        // }
        

        void GetInputs()
        {
            var lines = File.ReadAllLines("input.txt").ToList();
            Console.WriteLine($"Read {lines.Count} lines");

            _inputRecords = lines.Select(line => new InputRecord(line)).ToList();
        }
    }

    public class InputRecord
    {
        public InputRecord(string input)
        {
            var tokens = input.Split("|");
            Patterns = tokens[0].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
            ScrambledOutputs = tokens[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        }

        public List<string> Patterns { get; set; }
        public List<string> ScrambledOutputs { get; set; }

        public int CountSimpleOutputs()
        {
            return ScrambledOutputs.Count(IsUniqueOutput);
        }

        bool IsUniqueOutput(string pattern)
        {
            var uniquePatternLengths = new List<int> {2, 3, 4, 7};

            return uniquePatternLengths.Contains(pattern.Length);
        }
    }
}