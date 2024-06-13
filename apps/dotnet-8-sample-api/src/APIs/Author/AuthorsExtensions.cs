using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;

namespace Dotnet_8SampleApiDotNet.APIs.Extensions;

public static class AuthorsExtensions
{
    public static AuthorDto ToDto(this Author model)
    {
        return new AuthorDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Name = model.Name,
            TodoItems = model.TodoItems?.Select(x => new TodoItemIdDto { Id = x.Id }).ToList(),
        };
    }

    public static Author ToModel(this AuthorUpdateInput updateDto, AuthorIdDto idDto)
    {
        var author = new Author { Id = idDto.Id, Name = updateDto.Name };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            author.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            author.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return author;
    }
}
