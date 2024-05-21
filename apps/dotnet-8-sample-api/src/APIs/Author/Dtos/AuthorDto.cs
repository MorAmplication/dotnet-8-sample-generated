namespace Author.APIs.Dtos;

public class AuthorDto : AuthorIdDto
{
    public DateTime createdAt { get; }

    public string updatedAt { get; }

    public string? name { get; }

    public List<TodoItemDto>? todoItems { get; }
}
