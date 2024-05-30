namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public class TodoItemDto : TodoItemIdDto
{
    public DateTime CreatedAt { get; set; }

    public string UpdatedAt { get; set; }

    public List<AuthorIdDto>? Authors { get; set; }

    public WorkspaceIdDto? Workspace { get; set; }

    public bool? IsCompleted { get; set; }
}
