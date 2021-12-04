using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day03
{
    public class Solver
    {
        List<string> _records = new();

        public Solver()
        {
            GetInputs();
        }

        public double Solve1()
        {
            var gammaRate = 0;

            var recordLength = _records.First().Length;
            for (var i = 0; i < recordLength; i++)
            {
                if (i != 0) gammaRate <<= 1;

                var gammaVal = GetGammaValueForBit(i);
                gammaRate |= gammaVal;
            }

            var epsilonRate = CalculateEpsilonRate(gammaRate, recordLength);

            return gammaRate * epsilonRate;
        }

        int CalculateEpsilonRate(int gammaRate, int recordLength)
        {
            var mask = (int) Math.Pow(2, recordLength) - 1;
            var temp = ~gammaRate;

            return temp & mask;
        }

        public static string ToBinaryString(int num)
        {
            return Convert.ToString(num, 2).PadLeft(32, '0');
        }

        // public int Solve2()
        // {
        //     var horizontal = 0;
        //     var depth = 0;
        //     var aim = 0;
        //
        //     foreach (var instruction in _instructions)
        //     {
        //         switch (instruction.Direction)
        //         {
        //             case "forward":
        //                 horizontal += instruction.Distance;
        //                 depth += instruction.Distance * aim;
        //                 break;
        //             case "down":
        //                 aim += instruction.Distance;
        //                 break;
        //             case "up":
        //                 aim -= instruction.Distance;
        //                 break;
        //         }
        //     }
        //
        //     return horizontal * depth;
        // }

        int GetGammaValueForBit(int i)
        {
            var ones = _records.Count(x => x[i] == '1');
            var zeroes = _records.Count(x => x[i] == '0');

            return ones > zeroes ? 1 : 0;
        }

        void GetInputs()
        {
            var lines = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {lines.Length} inputs");

            _records = lines.ToList();
        }
    }
}