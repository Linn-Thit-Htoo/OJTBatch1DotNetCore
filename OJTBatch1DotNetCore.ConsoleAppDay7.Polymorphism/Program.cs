namespace OJTBatch1DotNetCore.ConsoleAppDay7.Polymorphism;

public class Animal
{
    public virtual void MakeSound() => Console.WriteLine("Animal is making sound...");
}

public class Dog : Animal
{
    public override void MakeSound() => Console.WriteLine("Dog is making sound...");
}

public class Cat : Animal
{
    public override void MakeSound() => Console.WriteLine("Cat is making sound...");
}

public class Program
{
    public static void Main(string[] args)
    {
        Animal dog = new Dog();
        dog.MakeSound();
    }
}
