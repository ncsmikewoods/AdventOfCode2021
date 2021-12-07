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
            return CalculateDangerZones(false);
        }

        public int Solve2()
        {
            return CalculateDangerZones(true);
        }

        int CalculateDangerZones(bool includeDiagonals)
        {
            var points = new Dictionary<string, int>();

            foreach (var line in _lines)
            {
                var segment = new Segment(line);

                if (IsDiagonal(segment) && !includeDiagonals) continue;

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
}