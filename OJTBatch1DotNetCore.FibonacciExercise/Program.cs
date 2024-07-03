namespace OJTBatch1DotNetCore.FibonacciExercise;

public class Program
{
    public static void Main(string[] args)
    {
        //  0, 1, 1, 2, 3, 5
        int firstNum = 0;
        int secondNum = 1;

        for (int i = 1; i <= 10; i++)
        {
            int sum = firstNum + secondNum; // 1, 2, 3, 5
            if (i == 1)
            {
                Console.Write($"{firstNum}, {secondNum}, {sum}, "); // 0, 1, 1
                //Console.Write($"{firstNum}, {secondNum}, {sum} "); // 0, 1, 1
            }
            else
            {
                Console.Write(i == 10 ? sum : sum + ", ");
                //Console.Write(", " + sum);
            }
            firstNum = secondNum;
            secondNum = sum;
        }
    }
}
