using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day02
{
    public class Solver
    {
        readonly List<Instruction> _instructions = new();

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            var horizontal = 0;
            var depth = 0;

            foreach(var instruction in _instructions)
            {
                switch (instruction.Direction)
                {
                    case "forward": 
                        horizontal += instruction.Distance;
                        break;
                    case "down":
                        depth += instruction.Distance;
                        break;
                    case "up":
                        depth -= instruction.Distance;
                        break;
                }
            }

            return horizontal * depth;
        }

        public int Solve2()
        {
            var horizontal = 0;
            var depth = 0;
            var aim = 0;

            foreach (var instruction in _instructions)
            {
                switch (instruction.Direction)
                {
                    case "forward":
                        horizontal += instruction.Distance;
                        depth += instruction.Distance * aim;
                        break;
                    case "down":
                        aim += instruction.Distance;
                        break;
                    case "up":
                        aim -= instruction.Distance;
                        break;
                }
            }

            return horizontal * depth;
        }

        void GetInputs()
        {
            var lines = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {lines.Length} inputs");

            foreach(var line in lines)
            {
                var tokens = line.Split();

                _instructions.Add(new Instruction
                {
                    Direction = tokens[0],
                    Distance = int.Parse(tokens[1])
                });
            }
        }
    }

    public class Instruction
    {
        public string Direction { get; set; }
        public int Distance { get; set; }
    }
}