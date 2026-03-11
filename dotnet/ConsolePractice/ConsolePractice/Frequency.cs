
using System;
class Frequency
{
    static void Main()
    {
        Console.Write("Enter size of array: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int[] arr = new int[n];
        bool[] used = new bool[n];

        Console.WriteLine("Enter array elements:");

        for (int i = 0; i < n; i++)
        {
            arr[i] = Convert.ToInt32(Console.ReadLine());
        }

        for (int i = 0; i < n; i++)
        {
            if (used[i] == true)
            {
                continue;
            }

            int count = 1;

            for (int j = i + 1; j < n; j++)
            {
                if (arr[i] == arr[j])
                {
                    count++;
                    used[j] = true;
                }
            }

            Console.WriteLine(arr[i] +"--repeated--" + count);
        }
    }
}