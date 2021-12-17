using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day09
{
    public class Solver
    {
        int[,] _points;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            var lowPoints = new List<int>();

            for (var i = 0; i < _points.GetLength(0); i++)
            {
                for (var j = 0; j < _points.GetLength(1); j++)
                {
                    if (IsLocalMin(i, j))
                    {
                        Console.WriteLine($"{i},{j} with a value of {_points[i, j]} is a local min");
                        lowPoints.Add(_points[i, j]);
                    }
                }
            }

            return lowPoints
                .Select(x => x+1)
                .Sum();
        }

        // public int Solve2()
        // {
        // }

        bool IsLocalMin(int x, int y)
        {
            var here = _points[x, y];

            var neighbors = new List<int>();

            // above
            if (y != 0)
            {
                neighbors.Add(_points[x, y - 1]);
            }

            // below
            if (y != _points.GetLength(1) - 1)
            {
                neighbors.Add(_points[x, y + 1]);
            }

            // left
            if (x != 0)
            {
                neighbors.Add(_points[x - 1, y]);
            }

            // right
            if (x != _points.GetLength(0) - 1)
            {
                neighbors.Add(_points[x + 1, y]);
            }

            var isLocalMin = neighbors.All(neighbor => here < neighbor);
            return isLocalMin;
        }

        void GetInputs()
        {
            var lines = File.ReadAllLines("input.txt").ToList();
            Console.WriteLine($"Read {lines.Count} lines");

            var width = lines.First().Length;
            var height = lines.Count;

            _points = new int[width, height];

            // Print(_points);

            for (var x = 0; x < width; x++)
            {
                for (var y = 0; y < height; y++)
                {
                    _points[x, y] = int.Parse(lines[y][x].ToString());
                    // Print(_points);
                }
            }

            Console.WriteLine("Points:");
            // Print(_points);
        }

        public static void Print<T>(T[,] matrix)
        {
            Console.WriteLine($"matrix dimensions: {matrix.GetLength(0)}x{matrix.GetLength(1)}");

            for (var j = 0; j < matrix.GetLength(1); j++)
            {
                for (var i = 0; i < matrix.GetLength(0); i++)
                {
                    Console.Write(matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("---------------------");
        }
    }
}