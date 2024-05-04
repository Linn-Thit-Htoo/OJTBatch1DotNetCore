using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OJTBatch1DotNetCore.SampleApi.Data;

namespace OJTBatch1DotNetCore.SampleApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public BlogController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        try
        {
            return Ok(await _appDbContext.Blogs
                .AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .ToListAsync());
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}