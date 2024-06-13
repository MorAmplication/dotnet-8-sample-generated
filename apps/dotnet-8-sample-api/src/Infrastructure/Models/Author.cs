using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_8SampleApiDotNet.Infrastructure.Models;

[Table("Authors")]
public class Author
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<TodoItem>? TodoItems { get; set; } = new List<TodoItem>();
}
