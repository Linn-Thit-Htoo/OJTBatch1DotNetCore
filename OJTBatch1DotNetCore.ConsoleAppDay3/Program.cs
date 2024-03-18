public class Program
{
    public static void Main(string[] args)
    {
        //Console.WriteLine("Please enter your name: ");
        //string? name = Console.ReadLine();

        //Console.WriteLine("Please enter your email: ");
        //string? email = Console.ReadLine();

        //Console.WriteLine("Please enter your password: ");
        //string? password = Console.ReadLine();

        //if (IsNullOrEmpty(name) || IsNullOrEmpty(email) || IsNullOrEmpty(password))
        //{
        //    Console.WriteLine("Please fill all fields...");
        //    return;
        //}

        string role = "member";
        switch (role)
        {
            case "user":
                Console.WriteLine("Role is user.");
                break;
            case "member":
                Console.WriteLine("Role is member.");
                break;
            case "admin":
                Console.WriteLine("Role is admin.");
                break;
            default:
                Console.WriteLine("");
                break;
        }
    }

    private static bool IsNullOrEmpty(string? str)
    {
        return string.IsNullOrEmpty(str);
    }
}