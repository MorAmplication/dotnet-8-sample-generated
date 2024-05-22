namespace Dotnet_8SampleApiDotNet.Infrastructure.Models;

[Table("Workspaces")]
public class Workspace
{
    [Key()]
    [required()]
    public string id { get; }

    [required()]
    public DateTime createdAt { get; }

    [required()]
    public string updatedAt { get; }

    public List<TodoItem> todoItems { get; } = new List<TodoItem>();

    [StringLength(1000)]
    public string name { get; }
}
