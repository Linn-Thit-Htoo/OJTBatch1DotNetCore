public class Program
{
    public static string _name = "Leo";
    public static void Main(string[] args)
    {
        string name = "Linn Thit";
        Console.WriteLine(name.Length);

        Console.WriteLine(name.ToUpper());
        Console.WriteLine(name.ToLower());

        // enum => category => MeDicines => consistent format


        string firstName = "Linn Thit";
        string lastName = "Htoo";

        // string concatenation
        Console.WriteLine(firstName + lastName);

        // string interpolation
        Console.WriteLine($"Your name is: {firstName + lastName}");


        // single responsibility SRP
        // IsNullOrEmpty(), Login(), Register(),

        Sum(b: 2, a: 1); // parameters int a, int b, int c, (a: 1, c: 3)
        Program.Login(email: "linnthit77387@gmail.com", password: "123123", name: "Leo");

        Console.ReadKey();
    }


    // int return
    public static int Sum(int a, int b, int c = 0) // arguments
    {
        return a + b;
    }

    public void Greeting() // no return
    {
        Console.WriteLine("Hello!");
        Hello();
        Login(email: "linnthit77387@gmail.com", password: "123123", name: "Leo");
        Console.WriteLine(Program._name);
    }

    public void Hello()
    {
        Console.WriteLine("");
    }

    public static string Login(string email, string password, string name = "")
    {
        return "testing...";
    }
}