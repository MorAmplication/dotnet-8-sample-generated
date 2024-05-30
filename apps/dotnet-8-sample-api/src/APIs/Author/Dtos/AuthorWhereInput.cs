namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public class AuthorWhereInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UpdatedAt { get; set; }

    public string? Name { get; set; }

    public List<TodoItemIdDto>? TodoItems { get; set; }
}
