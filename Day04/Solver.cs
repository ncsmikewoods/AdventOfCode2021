using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                if (winner == default) continue;

                var boardScore = winner.CalculateScore();
                return draw * boardScore;
            }

            return 0;
        }

        public int Solve2()
        {
            var solvingBoards = new List<Board>(_boards);

            while (solvingBoards.Count > 0)
            {
                foreach (var draw in _draws)
                {
                    foreach (var board in solvingBoards)
                    {
                        board.PickNumber(draw);
                    }

                    var winner = solvingBoards.FirstOrDefault(x => x.HasBingo());
                    if (winner == default) continue;

                    if (solvingBoards.Count == 1)
                    {
                        return draw * winner.CalculateScore();
                    }

                    solvingBoards.Remove(winner);
                    break;
                }
            }
            
            Console.WriteLine("We shouldn't have gotten here");
            return 0;
        }

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