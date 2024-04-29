using Microsoft.AspNetCore.Mvc;

namespace OJTBatch1DotNetCore.RestApiDemo.Controllers
{
    // /api/User
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Greeting()
        {
            return StatusCode(StatusCodes.Status200OK, "Welcome!");
        }
    }
}