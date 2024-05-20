namespace OJTBatch1DotNetCore.ConsoleAppDay7.Inheritance;

public class Animal
{
    public string _animalName = "Aung Aung";
    private int _animalAge;
    protected string _color = "red";

    public void MakeSound() => Console.WriteLine("Animal is making sound...");
}
public class Dog : Animal
{
    public void SetColor(string color)
    {
        _color = color;
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Dog dog = new();
        Console.WriteLine(dog._animalName);
        dog.MakeSound();
    }
}