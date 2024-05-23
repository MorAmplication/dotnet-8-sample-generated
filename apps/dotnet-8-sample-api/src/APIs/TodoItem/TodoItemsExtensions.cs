using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;

namespace Dotnet_8SampleApiDotNet.APIs.Extensions;

public class TodoItemsExtensions
{
    public static TodoItem ToModel(this TodoItemUpdateInput updateDto, TodoItemIdDto idDto)
    {
        var todoItem = new TodoItem { Id = idDto.Id, Name = updateDto.Name, };
        return todoItem;
    }

    public static TodoItemDto ToDto(this TodoItem model)
    {
        return new TodoItemDto
        {
            id = model.id,
            createdAt = model.createdAt,
            updatedAt = model.updatedAt,
            authors = model.Authors.Select(x => new AuthorIdDto { Id = x.Id }).ToList(),
            workspace = new WorkspaceIdDto { Id = model.WorkspaceId },
            isCompleted = model.isCompleted,
        };
    }
}
