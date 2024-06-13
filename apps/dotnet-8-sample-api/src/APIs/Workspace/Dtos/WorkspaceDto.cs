namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public class WorkspaceDto : WorkspaceIdDto
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<TodoItemIdDto>? TodoItems { get; set; }

    public string? Name { get; set; }
}
