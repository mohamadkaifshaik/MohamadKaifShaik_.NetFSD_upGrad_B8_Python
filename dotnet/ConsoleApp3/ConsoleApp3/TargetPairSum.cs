using System;

class TargetPairSum
{
    static void Main()
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        int target = 6;
        int n = arr.Length;

        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (arr[i] + arr[j] == target)
                {
                    Console.WriteLine(arr[i] + " + " + arr[j]);
                }
            }
        }
    }
}
