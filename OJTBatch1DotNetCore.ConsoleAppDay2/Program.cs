// naming conventions

// userName => camelCase
// UserName => Pascal
// user_name => snake_case
public class Program
{
    public static void Main(String[] args)
    {
        //string message = Program.Greeting();
        //Console.WriteLine(message);

        //Console.WriteLine("Plese enter your name: ");
        //string? name = Console.ReadLine(); // allow null
        //Console.WriteLine($"Your name is: {name}");

        //Console.WriteLine("Please enter your age: ");
        ////string? stringAge = Console.ReadLine(); ctrl + K + C & ctrl + K + U
        //int age = Convert.ToInt32(Console.ReadLine());
        //Console.WriteLine("Your age is: " + age);

        // Convert.ToInt32(Console.ReadLine())
        // operators
        string _email = "linnthit77387@gmail.com";
        string _password = "123123";

        Console.WriteLine("Plese enter your email: ");
        string? email = Console.ReadLine();

        Console.WriteLine("Plese enter your password: ");
        string? password = Console.ReadLine();

        // validation
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Please fill all fields...");
            return;
        }


        if (email == _email && password == _password) // comparison
        {
            Console.WriteLine("Login Successful!");
            return;
        }
        Console.WriteLine("Invalid User!");

        //Console.WriteLine(email == _email ? "Email is same!" : "Email invalid!");

        Console.ReadKey();
    }

    public static string Greeting()
    {
        return "Hello";
    }
}
