using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_8SampleApiDotNet.Infrastructure.Models;

[Table("TodoItems")]
public class TodoItem
{
    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Required()]
    public string UpdatedAt { get; set; }

    public List<Author>? Authors { get; set; } = new List<Author>();

    public string WorkspaceId { get; set; }

    [ForeignKey(nameof(WorkspaceId))]
    public Workspace? Workspace { get; set; } = null;

    public bool? IsCompleted { get; set; }
}
