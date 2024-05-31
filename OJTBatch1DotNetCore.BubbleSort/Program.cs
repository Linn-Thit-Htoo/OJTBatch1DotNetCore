namespace OJTBatch1DotNetCore.BubbleSort;

public class Program
{
    public static void Main(string[] args)
    {
        int[] nums = { 4, 3, 2, 1 };

        bool isSorted = false;
        while (!isSorted)
        {
            isSorted = true;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                int previousNum = nums[i]; // 4
                int nextNum = nums[i + 1]; // 3

                if (nextNum < previousNum)
                {
                    nums[i] = nextNum;
                    nums[i + 1] = previousNum;
                    isSorted = false;
                }
            }
        }

        foreach (var num in nums)
        {
            Console.WriteLine(num);
        }
    }
}