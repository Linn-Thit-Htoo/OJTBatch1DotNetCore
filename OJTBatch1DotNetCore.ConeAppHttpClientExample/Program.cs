using Newtonsoft.Json;
using OJTBatch1DotNetCore.ConeAppHttpClientExample;

public class Program
{
    public static async Task Main(string[] args)
    {
        await FetchData();
    }
    private async static Task FetchData()
    {
        // http client (axios or fetch)
        HttpClient client = new();
        var response = await client.GetAsync("https://ojtbatch1.bsite.net/api/Blog");
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            List<BlogDataModel> lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
            foreach (var item in lst)
            {
                await Console.Out.WriteLineAsync($"BlogId: {item.BlogId}");
                await Console.Out.WriteLineAsync($"BlogTitle: {item.BlogTitle}");
                await Console.Out.WriteLineAsync($"BlogAuthor: {item.BlogAuthor}");
                await Console.Out.WriteLineAsync($"BlogContent: {item.BlogContent}");
                await Console.Out.WriteLineAsync();
            }
        }
    }
}