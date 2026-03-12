using System;

class ArrayToRight
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        int n = arr.Length;

        int last = arr[n - 1];

        for (int i = n - 1; i > 0; i--)
        {
            arr[i] = arr[i - 1];
        }

        arr[0] = last;

        for (int i = 0; i < n; i++)
        {
            Console.Write(arr[i] + " ");
        }
    }
}