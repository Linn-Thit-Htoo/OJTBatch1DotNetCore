using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OJTBatch1DotNetCore.SampleApi.SampleMvc.Models;

namespace OJTBatch1DotNetCore.SampleApi.SampleMvc.Controllers
{
    public class BlogController : Controller
    {
        private readonly HttpClient _httpClient;

        public BlogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                List<BlogDataModel> lst = new();
                HttpResponseMessage response = await _httpClient.GetAsync("/api/Blog");
                if (response.IsSuccessStatusCode)
                {
                    string jsonStr = await response.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<BlogDataModel>>(jsonStr)!;
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
