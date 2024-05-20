using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OJTBatch1DotNetCore.EFCoreSample.Data;
using OJTBatch1DotNetCore.EFCoreSample.Models;

namespace OJTBatch1DotNetCore.EFCoreSample.Controllers;

public class CategoryController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public CategoryController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    #region Get Categories

    [HttpGet]
    [Route("/api/category")]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            List<CategoryModel> lst = await _appDbContext.Categories
                .AsNoTracking() // with no lock
                .ToListAsync();
            return Ok(lst);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Create Category

    [HttpPost]
    [Route("/api/category")]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryModel requestModel)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.CategoryName))
                return BadRequest();

            var item = await _appDbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CategoryName == requestModel.CategoryName && x.IsActive);
            if (item is not null)
                return Conflict("Category Name already exists!");

            await _appDbContext.Categories.AddAsync(requestModel);
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0 ? StatusCode(201, "Creating Successful!") : BadRequest("Creating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    [HttpPut]
    [Route("/api/category/{id}")]
    public async Task<IActionResult> UpdateCategory([FromBody] CategoryRequestModel requestModel, long id)
    {
        try
        {
            if (string.IsNullOrEmpty(requestModel.CategoryName) || id <= 0)
                return BadRequest();

            bool isDuplicate = await _appDbContext.Categories
                .AsNoTracking()
                .AnyAsync(x => x.CategoryName == requestModel.CategoryName && x.IsActive && x.CategoryId != id);
            if (isDuplicate)
                return Conflict("Category Name already exists!");

            var item = await _appDbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CategoryId == id && x.IsActive);
            if (item is null)
                return NotFound("Category Not Found or Inactive!");

            item.CategoryName = requestModel.CategoryName;
            _appDbContext.Entry(item).State = EntityState.Modified;
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0 ? StatusCode(202, "Updating Successful!") : BadRequest("Updating Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpDelete]
    [Route("/api/category/{id}")]
    public async Task<IActionResult> DeleteCategory(long id)
    {
        try
        {
            if (id <= 0)
                return BadRequest();

            var item = await _appDbContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CategoryId == id && x.IsActive);
            if (item is null)
                return NotFound("Category Not Found or Inactive!");

            item.IsActive = false;
            _appDbContext.Entry(item).State = EntityState.Modified;
            int result = await _appDbContext.SaveChangesAsync();

            return result > 0 ? StatusCode(202, "Deleting Successful!") : BadRequest("Deleting Fail!");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}