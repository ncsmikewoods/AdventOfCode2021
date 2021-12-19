using System;

namespace Day10
{
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