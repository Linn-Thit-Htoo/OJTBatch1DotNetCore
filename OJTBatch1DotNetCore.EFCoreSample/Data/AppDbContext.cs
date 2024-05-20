using Microsoft.EntityFrameworkCore;
using OJTBatch1DotNetCore.EFCoreSample.Models;

namespace OJTBatch1DotNetCore.EFCoreSample.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<CategoryModel> Categories { get; set; }
}