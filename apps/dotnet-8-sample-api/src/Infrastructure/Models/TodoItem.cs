namespace Dotnet_8SampleApiDotNet.Infrastructure.Models;

[Table("TodoItems")]
public class TodoItem
{
    [Key()]
    [required()]
    public string id { get; }

    [required()]
    public DateTime createdAt { get; }

    [required()]
    public string updatedAt { get; }

    public List<Author> authors { get; } = new List<Author>();

    public Workspace workspaceId { get; }

    [ForeignKey(nameof(workspaceId))]
    public Workspace workspace { get; } = null;

    public bool isCompleted { get; }
}
