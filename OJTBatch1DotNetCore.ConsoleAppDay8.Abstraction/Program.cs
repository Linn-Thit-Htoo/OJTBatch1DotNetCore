namespace OJTBatch1DotNetCore.ConsoleAppDay8.Abstraction;

public abstract class Animal
{
    public abstract void MakeSound();
    public void Eat() => Console.WriteLine("Animal is eating..");
}

public class Dog : Animal
{
    public override void MakeSound()
    {
        Console.WriteLine("Dog is barking...");
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Dog dog = new();
        dog.MakeSound();
        dog.Eat();
    }
}