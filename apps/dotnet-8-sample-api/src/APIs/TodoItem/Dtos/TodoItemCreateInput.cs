namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public class TodoItemCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<AuthorIdDto>? Authors { get; set; }

    public WorkspaceIdDto? Workspace { get; set; }

    public bool? IsCompleted { get; set; }
}
