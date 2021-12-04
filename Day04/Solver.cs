using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace Day04
{
    public class Solver
    {
        List<int> _draws = new();
        List<Board> _boards;

        public Solver()
        {
            GetInputs();
        }

        public int Solve1()
        {
            foreach (var draw in _draws)
            {
                foreach (var board in _boards)
                {
                    board.PickNumber(draw);
                }

                var winner = _boards.FirstOrDefault(x => x.HasBingo());

                if (winner != default)
                {
                    var boardScore = winner.CalculateScore();
                    return draw * boardScore;
                }
            }

            return 0;
        }

        // public double Solve2()
        // {
        //     var oxygenRating = Convert.ToInt32(GetOxygenRating(_records), 2);
        //     var c02Rating = Convert.ToInt32(GetC02Rating(_records), 2);
        //
        //     return oxygenRating * c02Rating;
        // }

        void GetInputs()
        {
            var text = File.ReadAllText("input.txt");

            var chunks = text.Split($"{Environment.NewLine}{Environment.NewLine}");
            _draws = chunks[0].Split(',').Select(int.Parse).ToList();

            _boards = new List<Board>();

            foreach (var rawBoard in chunks[1..])
            {
                _boards.Add(new Board(rawBoard));
            }

            Console.WriteLine($"Draws: {_draws.Count}, Boards: {_boards.Count}");
        }
    }
}