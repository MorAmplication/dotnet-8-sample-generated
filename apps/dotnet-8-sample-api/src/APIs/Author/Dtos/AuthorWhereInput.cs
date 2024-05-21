namespace Author.APIs.Dtos;

public class AuthorWhereInput
{
    public string id { get; }

    public DateTime createdAt { get; }

    public string updatedAt { get; }

    public string? name { get; }

    public List<TodoItemDto>? todoItems { get; }
}
