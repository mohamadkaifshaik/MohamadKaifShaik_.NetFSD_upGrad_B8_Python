using System;

class SumAll
{
    static void Main()
    {
        int[,] matrix =
        {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        int rows = 3;
        int cols = 3;
        int sum = 0;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                sum = sum + matrix[i, j];
            }
        }

        Console.WriteLine("Sum of all elements: " + sum);
    }
}