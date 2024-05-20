using Microsoft.AspNetCore.Mvc;

namespace MvcSample.Controllers;

public class TestController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}