using System;
using System.Linq;

namespace Day04
{
    public class Board
    {
        readonly Cell[,] _board = new Cell[5, 5];

        public Board(string input)
        {
            var delimiters = new[] {" ", Environment.NewLine};
            var cells = input
                .Split(delimiters, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(x => new Cell(x))
                .ToArray();

            var index = 0;
            for (var rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                for (var colIndex = 0; colIndex < 5; colIndex++)
                {
                    _board[rowIndex, colIndex] = cells[index++];
                }
            }
        }

        public void PickNumber(int num)
        {
            for (var rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                for (var colIndex = 0; colIndex < 5; colIndex++)
                {
                    var cell = _board[rowIndex, colIndex];
                    if (cell.Number == num)
                    {
                        cell.IsMarked = true;
                    }
                }
            }
        }

        public bool HasBingo()
        {
            // check for rows
            for (var rowIndex = 0; rowIndex < 5; rowIndex++)
            {
                if (_board[rowIndex, 0].IsMarked 
                    && _board[rowIndex, 1].IsMarked 
                    && _board[rowIndex, 2].IsMarked 
                    && _board[rowIndex, 3].IsMarked 
                    && _board[rowIndex, 4].IsMarked)
                {
                    return true;
                }
            }

            // check for columns
            for (var colIndex = 0; colIndex < 5; colIndex++)
            {
                if (_board[0, colIndex].IsMarked
                    && _board[1, colIndex].IsMarked
                    && _board[2, colIndex].IsMarked
                    && _board[3, colIndex].IsMarked
                    && _board[4, colIndex].IsMarked)
                {
                    return true;
                }
            }

            return false;
        }

        public int CalculateScore()
        {
            var flattened = _board.Cast<Cell>();

            return flattened
                .Where(x => !x.IsMarked)
                .Select(y => y.Number)
                .Sum();
        }
    }
}