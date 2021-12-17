using System;
using System.Collections.Generic;
using System.Linq;

namespace Day08
{
    public class InputRecord
    {
        public InputRecord(string input)
        {
            var tokens = input.Split("|");

            Patterns = tokens[0]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => new string(x.OrderBy(c => c).ToArray()))
                .ToList();

            ScrambledOutputs = tokens[1]
                .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => new string(x.OrderBy(c => c).ToArray()))
                .ToList();

            DeriveCodes();
        }

        public List<string> Patterns { get; set; }
        public List<string> ScrambledOutputs { get; set; }

        public Dictionary<int, string> Code { get; set; } = new();

        public Dictionary<string, int> PatternToNumber
        {
            get
            {
                // Reverse the Code dictionary
                return Code.ToDictionary(x => x.Value, x => x.Key);
            }
        }

        public int CountSimpleOutputs()
        {
            return ScrambledOutputs.Count(IsUniqueOutput);
        }

        static bool IsUniqueOutput(string pattern)
        {
            var uniquePatternLengths = new List<int> {2, 3, 4, 7};

            return uniquePatternLengths.Contains(pattern.Length);
        }

        public int DescrambleOutputs()
        {
            var stringNumber = string.Join("", ScrambledOutputs.Select(x => PatternToNumber[x]));
            return int.Parse(stringNumber);
        }

        void DeriveCodes()
        {
            // Easy unique ones
            Code[1] = Patterns.First(x => x.Length == 2);
            Code[7] = Patterns.First(x => x.Length == 3);
            Code[4] = Patterns.First(x => x.Length == 4);
            Code[8] = Patterns.First(x => x.Length == 7);

            // get what we can out of the 5 length patterns
            ///////////////////////////////////////////////
            var theFiveLengths = Patterns.Where(x => x.Length == 5).ToList();

            // 3 is the only 5 length pattern that is length 3 when subtracting the 1 code from it
            Code[3] = theFiveLengths.Single(x => x.Except(Code[1]).ToList().Count == 3);

            // 2, 3, and 5 have only horizontal segments in common
            var horizontalSegments =
                string.Concat(theFiveLengths[0].Intersect(theFiveLengths[1]).Intersect(theFiveLengths[2]));

            var twoAndFive = theFiveLengths.Except(new List<string>{Code[3]}).ToList();

            // 5 can be distinguished from 2 by subtracting horizontal segments and 4 code from it to get 0 remaining segments
            Code[5] = twoAndFive.Single(x => x.Except(horizontalSegments).Except(Code[4]).ToList().Count == 0);

            // process of elimination for 2 (its length 5 and isn't 3 or 5)
            Code[2] = theFiveLengths.Single(x => x != Code[3] && x != Code[5]);


            // get what we can out of the 6 length patterns
            ///////////////////////////////////////////////
            var theSixLengths = Patterns.Where(x => x.Length == 6).ToList();

            // 0 is the only 6 length pattern that only has 4 non-horizontal segments
            Code[0] = theSixLengths.Single(x => x.Except(horizontalSegments).ToList().Count == 4);

            // 9 is the only 6 length pattern than is length 0 when subtracting the horizontals and 4 code from it
            Code[9] = theSixLengths.Single(x => x.Except(horizontalSegments).Except(Code[4]).ToList().Count == 0);

            // process of elimination for 6 (its length is 6 and isn't 0 or 9)
            Code[6] = theSixLengths.Single(x => x != Code[0] && x != Code[9]);
        }
    }
}