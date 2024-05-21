namespace TodoItem.APIs.Dtos;

public class TodoItemUpdateInput
{
    public string id { get; }

    public DateTime createdAt { get; }

    public string updatedAt { get; }

    public List<AuthorDto>? authors { get; }

    public WorkspaceDto? workspace { get; }

    public bool? isCompleted { get; }
}
