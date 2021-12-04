namespace Day04
{
    public static class ExtensionMethods
    {
        public static int[,] Transpose(this int[,] input)
        {
            var w = input.GetLength(0);
            var h = input.GetLength(1);

            var result = new int[h, w];

            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    result[j, i] = input[i, j];
                }
            }

            return result;
        }
    }
}