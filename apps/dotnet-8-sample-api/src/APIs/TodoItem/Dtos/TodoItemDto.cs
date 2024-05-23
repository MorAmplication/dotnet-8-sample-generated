namespace TodoItem.APIs.Dtos;

public class TodoItemDto : TodoItemIdDto
{
    public DateTime createdAt { get; }

    public string updatedAt { get; }

    public List<AuthorDto>? authors { get; }

    public WorkspaceDto? workspace { get; }

    public bool? isCompleted { get; }
}
