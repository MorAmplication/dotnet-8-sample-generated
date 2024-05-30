using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_8SampleApiDotNet.Infrastructure.Models;

[Table("Workspaces")]
public class Workspace
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public string UpdatedAt { get; set; }

    public List<TodoItem>? TodoItems { get; set; } = new List<TodoItem>();

    [StringLength(1000)]
    public string? Name { get; set; }
}
