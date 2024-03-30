namespace OJTBatch1DotNetCore.ConsoleAppDay7.Encapsulation
{
    public class Animal
    {
        private string _animalName = string.Empty;

        public void SetName(string animalName)
        {
            _animalName = animalName;
        }

        public string GetName()
        {
            return _animalName;
        }

        // setter using constructor
        //public Animal(string animalName = "Aung Aung")
        //{
        //    _animalName = animalName;
        //}
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            Animal animal = new();
            animal.SetName("Aung Aung");
            Console.WriteLine(animal.GetName());
        }
    }
}