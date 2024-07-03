namespace OJTBatch1DotNetCore.ConsoleAppDay8.Interface;

public interface IAnimal
{
    void MakeSound();
    void Eat();
    void Run();
}

public interface IMammal
{
    void Sleep();
}

public class Dog : IAnimal, IMammal
{
    public void Eat()
    {
        Console.WriteLine("Dog is eating...");
    }

    public void MakeSound()
    {
        Console.WriteLine("Dog is barking...");
    }

    public void Run()
    {
        Console.WriteLine("Dog is Running...");
    }

    public void Sleep()
    {
        Console.WriteLine("Dog is Sleeping...");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Dog dog = new();
        dog.MakeSound();
        dog.Eat();
        dog.Run();
        dog.Sleep();
    }
}
