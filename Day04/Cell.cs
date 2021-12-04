namespace Day04
{
    public class Cell
    {
        public int Number { get; set; }
        public bool IsMarked { get; set; }

        public Cell(string input)
        {
            Number = int.Parse(input);
        }
    }
}