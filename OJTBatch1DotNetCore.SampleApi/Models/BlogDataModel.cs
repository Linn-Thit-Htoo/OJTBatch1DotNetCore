using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OJTBatch1DotNetCore.SampleApi.Models;

[Table("Tbl_Blog")]
public class BlogDataModel
{
    [Key]
    public long BlogId { get; set; }
    public string BlogTitle { get; set; } = null!;
    public string BlogAuthor { get; set; } = null!;
    public string BlogContent { get; set; } = null!;
}
