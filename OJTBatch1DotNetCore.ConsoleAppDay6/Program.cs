using System.Security.Cryptography;

public class Program
{
    public static void Main(string[] args)
    {
        //Sum(1.1, 2.1, 3.1);
        Animal animal = new(""); // object intantiate
        Console.WriteLine(animal._name);

        Count count = new();
        count._count += 1;
        Count.staticCount += 1;

        Count count1 = new();
        count1._count += 1;
        Count.staticCount += 1;

        Console.WriteLine("Instance count: " + count._count);
        Console.WriteLine("Static count: " + Count.staticCount);

        int[] nums = { 10, 21, 19, 2, 5, 1, 7, 90 };
        int minVal = nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] < minVal)
            {
                minVal = nums[i];
            }
        }
        Console.WriteLine(minVal);

        List<int> lst1 = new()
        {
            1,
            2,
            3
        };

        Stack<int> stack1 = new();
        stack1.Push(1);

        List<int> lst2 = new()
        {
            4,5,6
        };

        lst1.AddRange(lst2);

        foreach (var item in lst1)
        {
            Console.WriteLine(item);
        }

        //Console.WriteLine(animal._name);
        //Count count = new();
        //count._count += 1;
        //Console.WriteLine(count._count);

        //Count count1 = new();
        //count1._count += 1;
        //Console.WriteLine(count1._count);

    }

    //public static int Sum(int a, int b)
    //{
    //    return a + b;
    //}

    //public static int Sum(int a, int b, int c)
    //{
    //    return a + b + c;
    //}
    //public static double Sum(double a, double b, double c)
    //{
    //    return a + b + c;
    //}
}

public class Animal
{
    public string _name = string.Empty;
    public int _age = 5;

    public Animal(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        _name = name;
    }
}

public class Count
{
    public int _count = 0;
    public static int staticCount = 0;
}