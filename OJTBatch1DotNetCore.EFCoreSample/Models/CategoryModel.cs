using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OJTBatch1DotNetCore.EFCoreSample.Models;

[Table("Category")]
public class CategoryModel
{
    [Key]
    public long CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
    public bool IsActive { get; set; }
}