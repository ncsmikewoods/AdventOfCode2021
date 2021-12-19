using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    public class Solver
    {
        List<string> _lines;

        public Solver()
        {
            GetInputs();
        }

        public double Solve1()
        {
            var syntaxErrorCount = 0d;

            foreach (var line in _lines)
            {
                try
                {
                    ParseLine(line);
                }
                catch (InvalidChunkException e)
                {
                    syntaxErrorCount += e.ErrorValue;
                }
            }

            return syntaxErrorCount;
        }

        static void ParseLine(string line)
        {
            var chunks = new Stack<char>();

            foreach (var character in line)
            {
                if (!IsTerminator(character))
                {
                    chunks.Push(character);
                    continue;
                }

                if (CompletesChunk(chunks, character))
                {
                    chunks.Pop();
                    continue;
                }

                Console.WriteLine($"Corrupted Line: {line}");
                throw new InvalidChunkException(character);
            }
        }

        static bool IsTerminator(char next)
        {
            return next is ')' or ']' or '}' or '>';
        }

        static bool CompletesChunk(Stack<char> chunks, char next)
        {
            if (chunks.Count == 0) return false;

            var last = chunks.Peek();

            return last switch
            {
                '(' => next == ')',
                '[' => next == ']',
                '{' => next == '}',
                '<' => next == '>'
            };
        }


        double CalculateErrorScore(int parenCount, int squareCount, int curlyCount, int angleCount)
        {
            var sum = 0d;
            sum += parenCount * 3;
            sum += squareCount * 57;
            sum += curlyCount * 1197;
            sum += angleCount * 25137;
            return sum;
        }

        // public int Solve2()
        // {
        //     
        // }

        
        void GetInputs()
        {
            _lines = File.ReadAllLines("input.txt").ToList();
            Console.WriteLine($"Read {_lines.Count} lines");
        }
    }

    public class InvalidChunkException : Exception
    {
        public InvalidChunkException(char character)
        {
            Character = character;

            ErrorValue = character switch
            {
                ')' => 3,
                ']' => 57,
                '}' => 1197,
                '>' => 25_137
            };
        }

        public char Character { get; set; }

        public int ErrorValue { get; set; }
    }
}