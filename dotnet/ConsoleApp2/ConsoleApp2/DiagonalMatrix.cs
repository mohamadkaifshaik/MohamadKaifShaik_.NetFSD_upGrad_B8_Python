using System;

class DiagonalMatrix
{
    static void Main()
    {
        int[,] matrix =
        {
            {1, 2, 3},
            {4, 5, 6},
            {7, 8, 9}
        };

        int n = 3;

        Console.WriteLine("Diagonal elements:");

        for (int i = 0; i < n; i++)
        {
            Console.Write(matrix[i, i] + " ");
        }
    }
}