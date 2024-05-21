namespace Workspace.APIs.Dtos;

public class WorkspaceDto : WorkspaceIdDto
{
    public DateTime createdAt { get; }

    public string updatedAt { get; }

    public List<TodoItemDto>? todoItems { get; }

    public string? name { get; }
}
