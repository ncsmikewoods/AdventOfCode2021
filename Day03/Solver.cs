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

                var gammaVal = GetMostCommonValueForBit(i, _records);
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

        int GetMostCommonValueForBit(int i, List<string> records)
        {
            var ones = records.Count(x => x[i] == '1');
            var zeroes = records.Count(x => x[i] == '0');

            return ones >= zeroes ? 1 : 0;
        }

        public double Solve2()
        {
            var oxygenRating = Convert.ToInt32(GetOxygenRating(_records), 2);
            var c02Rating = Convert.ToInt32(GetC02Rating(_records), 2);

            return oxygenRating * c02Rating;
        }

        string GetOxygenRating(List<string> records)
        {
            if (records.Count == 1)
            {
                return records.First();
            }

            var digit = GetMostCommonValueForBit(0, records);

            var filteredRecords = records.Where(x => x.StartsWith(digit.ToString())).ToList();
            var rest = GetOxygenRating(filteredRecords.Select(x => x[1..]).ToList());

            return digit + rest;
        }

        string GetC02Rating(List<string> records)
        {
            if (records.Count == 1)
            {
                return records.First();
            }

            var digit = GetMostCommonValueForBit(0, records) == 1 ? 0 : 1;

            var filteredRecords = records.Where(x => x.StartsWith(digit.ToString())).ToList();
            var rest = GetC02Rating(filteredRecords.Select(x => x[1..]).ToList());

            return digit + rest;
        }

        public static string ToBinaryString(int num)
        {
            return Convert.ToString(num, 2).PadLeft(32, '0');
        }

        void GetInputs()
        {
            var lines = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {lines.Length} inputs");

            _records = lines.ToList();
        }
    }
}