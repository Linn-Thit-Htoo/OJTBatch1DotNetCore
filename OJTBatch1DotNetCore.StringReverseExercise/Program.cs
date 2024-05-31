namespace OJTBatch1DotNetCore.StringReverseExercise;

public class Program
{
    public static void Main(string[] args)
    {
        // Helo => o l e H
        Console.WriteLine("Please enter your name: ");
        string? name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
            return;

        char[] charArray = name.ToCharArray(); // 4

        //Array.Reverse(charArray);
        //foreach (char c in charArray)
        //    Console.Write(c);

        char[] reversedArray = new char[charArray.Length]; // o l e H

        for (int i = 0; i < charArray.Length; i++)
        {
            // H e l o // 2 3
            reversedArray[i] = charArray[charArray.Length - 1 - i];
        }

        foreach (char c in reversedArray)
        {
            Console.Write(c);
        }

        //for (int i = name.Length - 1; i >= 0; i--)
        //{
        //    Console.Write(name[i]);
        //}
    }
}