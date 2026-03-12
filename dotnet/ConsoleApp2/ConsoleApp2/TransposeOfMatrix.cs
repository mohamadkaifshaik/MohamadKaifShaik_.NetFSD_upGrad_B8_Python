using System;

class TransposeOfMatrix
{
    static void Main()
    {
        int[,] matrix = {
            {1, 2, 3},
            {4, 5, 6}
        };

        int rows = 2;
        int cols = 3;

        int[,] transpose = new int[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transpose[j, i] = matrix[i, j];
            }
        }

        Console.WriteLine("Transpose of matrix:");

        for (int i = 0; i < cols; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Console.Write(transpose[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
}