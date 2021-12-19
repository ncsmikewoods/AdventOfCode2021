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

        public double Solve2()
        {
            var incompleteLines = new List<Stack<char>>();

            foreach (var line in _lines)
            {
                try
                {
                    incompleteLines.Add(ParseLine(line));
                }
                catch (InvalidChunkException)
                {
                }
            }

            var scores = incompleteLines.Select(CalculateCompletionScore).ToList();
            return GetMedian(scores);
        }

        static double GetMedian(List<double> scores)
        {
            scores.Sort();
            var midpoint = (int)Math.Floor(scores.Count / 2d);
            return scores.ElementAt(midpoint);
        }

        static double CalculateCompletionScore(Stack<char> stack)
        {
            var score = 0d;
            foreach (var c in stack)
            {
                score *= 5;
                score += c switch
                {
                    '(' => 1,
                    '[' => 2,
                    '{' => 3,
                    '<' => 4
                };
            }
            return score;
        }

        static Stack<char> ParseLine(string line)
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

                // Console.WriteLine($"Corrupted Line: {line}");
                throw new InvalidChunkException(character);
            }

            return chunks;
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

        void GetInputs()
        {
            _lines = File.ReadAllLines("input.txt").ToList();
            Console.WriteLine($"Read {_lines.Count} lines");
        }
    }
}