using System;
class Frequency
{
    static void Main()
    {
        int[] arr = { 1, 2, 2, 3, 1, 4, 2 };
        int n = arr.Length;

        for (int i = 0; i < n; i++)
        {
            int count = 0;

            for (int j = 0; j < n; j++)
            {
                if (arr[i] == arr[j])
                {
                    count++;
                }
            }

            if (i == 0 || arr[i] != arr[i - 1])
            {
                Console.WriteLine(arr[i] + " occurs " + count + " times");
            }
        }
    }
}
