using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;

namespace Dotnet_8SampleApiDotNet.APIs.Extensions;

public class AuthorsExtensions
{
    public static Author ToModel(this AuthorUpdateInput updateDto, AuthorIdDto idDto)
    {
        var author = new Author { Id = idDto.Id, Name = updateDto.Name, };
        return author;
    }

    public static AuthorDto ToDto(this Author model)
    {
        return new AuthorDto
        {
            id = model.id,
            createdAt = model.createdAt,
            updatedAt = model.updatedAt,
            name = model.name,
            todoItems = model.TodoItems.Select(x => new TodoItemIdDto { Id = x.Id }).ToList(),
        };
    }
}
