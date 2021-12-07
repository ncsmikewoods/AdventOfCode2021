using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    public class Solver
    {
        string[] _lines;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            var points = new Dictionary<string, int>();

            foreach (var line in _lines)
            {
                var segment = new Segment(line);
                
                if (IsDiagonal(segment)) continue;

                var containingPoints = segment.GetContainingPoints();

                foreach (var containingPoint in containingPoints)
                {
                    if (points.ContainsKey(containingPoint))
                    {
                        points[containingPoint]++;
                        continue;
                    }

                    points[containingPoint] = 1;
                }
            }

            Console.WriteLine($"Tracked points: {points.Keys.Count}");
            return points.Values.Count(x => x >= 2);
        }

        // public int Solve2()
        // {
        //     return 0;
        // }

        static bool IsDiagonal(Segment segment)
        {
            return !segment.IsHorizontal() && !segment.IsVertical();
        }

        void GetInputs()
        {
            _lines = File.ReadAllLines("input.txt");
            Console.WriteLine($"Read {_lines.Length} lines of input");
        }
    }

    public class Segment
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public Segment(string input)
        {
            var tokens = input
                .Split(new[] { " -> ", "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            StartX = tokens[0];
            StartY = tokens[1];
            EndX = tokens[2];
            EndY = tokens[3];
        }

        public bool IsHorizontal()
        {
            return StartY == EndY;
        }

        public bool IsVertical()
        {
            return StartX == EndX;
        }

        public List<string> GetContainingPoints()
        {
            if (IsHorizontal())
            {
                var xDistance = Math.Abs(StartX - EndX) + 1;
                var smallerX = StartX < EndX ? StartX : EndX;
                return Enumerable.Range(smallerX, xDistance)
                    .Select(x => $"{x},{StartY}")
                    .ToList();
            }

            var yDistance = Math.Abs(EndY - StartY) + 1;
            var smallerY = StartY < EndY ? StartY : EndY;
            return Enumerable.Range(smallerY, yDistance)
                .Select(y => $"{StartX},{y}")
                .ToList();
        }
    }
}