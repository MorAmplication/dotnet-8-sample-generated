namespace Workspace.APIs.Dtos;

public class WorkspaceWhereInput
{
    public string id { get; }

    public DateTime createdAt { get; }

    public string updatedAt { get; }

    public List<TodoItemDto>? todoItems { get; }

    public string? name { get; }
}
