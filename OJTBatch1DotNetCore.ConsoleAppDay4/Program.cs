using Newtonsoft.Json;

public class Program
{
    public static string _email = "linnthit77387@gmail.com";
    public static string _password = "123123";
    public static async Task Main(String[] agrs)
    {
        int[] ages = { 18, 19, 20, 21 }; // 0, 1, 2, 3
        List<string> names = new()
        {
            "Leo",
            "Linn Thit",
            "Kaung Htet Kyaw"
        };

        await GetDataAsync();

        //lst.ForEach(x => Console.WriteLine(x));

        //foreach(var item in lst)
        //{
        //    Console.WriteLine(item);
        //}

        //foreach (int age in ages)
        //{
        //    Console.WriteLine(age);
        //}

        // Array.ForEach(ages, age => Console.WriteLine(age)); // lambda function
        //Array.ForEach(ages, (age) =>
        //{
        //    Console.WriteLine(age);
        //});

        // i = 0 => initialization
        // i <= 9 => condition (endpoint)
        // i++ => post increment
        // condition => i < array.length 4 => 0,1,2,3
        //for (int i = 0; i <= 9; i++)
        //{
        //    Console.WriteLine(i);
        //}

        //int a = 0;
        //while (a < 5)
        //{
        //    Console.WriteLine(a);
        //    a++;
        //}


        //bool isPassed = false;
        //do
        //{
        //    Console.WriteLine("Pleae enter your email: ");
        //    string? email = Console.ReadLine();

        //    Console.WriteLine("Pleae enter your password: ");
        //    string? password = Console.ReadLine();

        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        //    {
        //        Console.WriteLine("Please fill all fields...");
        //        return;
        //    }

        //    if (email.Equals(_email) && password.Equals(_password))
        //    {
        //        isPassed = true;
        //        Console.WriteLine("Login Successful.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Login Fail.\n");
        //    }
        //} while (!isPassed);
    }

    // c# api integration
    public static async Task GetDataAsync()
    {
        HttpClient client = new();
        HttpResponseMessage response = await client.GetAsync("https://ojtbatch1.bsite.net/api/Blog");
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
            foreach (var item in lst)
            {
                await Console.Out.WriteLineAsync($"Blog Title: {item.BlogTitle}");
            }
        }
    }

    public class BlogDataModel
    {
        public long BlogId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public string BlogAuthor { get; set; } = null!;
        public string BlogContent { get; set; } = null!;
    }
}