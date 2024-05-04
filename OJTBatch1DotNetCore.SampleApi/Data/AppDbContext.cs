using Microsoft.EntityFrameworkCore;
using OJTBatch1DotNetCore.SampleApi.Models;

namespace OJTBatch1DotNetCore.SampleApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
