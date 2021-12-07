using System;
using System.Collections.Generic;
using System.Linq;

namespace Day05
{
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
                return CalculateHorizontalPoints();
            }

            if (IsVertical())
            {
                return CalculateVerticalContainingPoints();
            }

            return CalculateDiagonalContainingPoints();
        }

        List<string> CalculateDiagonalContainingPoints()
        {
            var distance = Math.Abs(StartX - EndX) + 1;
            var smallerX = StartX < EndX ? StartX : EndX;
            var smallerY = StartY < EndY ? StartY : EndY;

            var xPoints = Enumerable.Range(smallerX, distance);
            var yPoints = Enumerable.Range(smallerY, distance);

            if (EndX < StartX) xPoints = xPoints.Reverse();
            if (EndY < StartY) yPoints = yPoints.Reverse();

            return xPoints
                .Zip(yPoints, (x, y) => $"{x},{y}")
                .ToList();
        }

        List<string> CalculateVerticalContainingPoints()
        {
            var yDistance = Math.Abs(EndY - StartY) + 1;
            var smallerY = StartY < EndY ? StartY : EndY;
            return Enumerable.Range(smallerY, yDistance)
                .Select(y => $"{StartX},{y}")
                .ToList();
        }

        List<string> CalculateHorizontalPoints()
        {
            var xDistance = Math.Abs(StartX - EndX) + 1;
            var smallerX = StartX < EndX ? StartX : EndX;
            return Enumerable.Range(smallerX, xDistance)
                .Select(x => $"{x},{StartY}")
                .ToList();
        }
    }
}