namespace Dotnet_8SampleApiDotNet.Infrastructure.Models;

[Table("Authors")]
public class Author
{
    [Key()]
    [required()]
    public string id { get; }

    [required()]
    public DateTime createdAt { get; }

    [required()]
    public string updatedAt { get; }

    [StringLength(1000)]
    public string name { get; }

    public List<TodoItem> todoItems { get; } = new List<TodoItem>();
}
