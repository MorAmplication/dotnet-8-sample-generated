using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;

namespace Dotnet_8SampleApiDotNet.APIs.Extensions;

public static class TodoItemsExtensions
{
    public static TodoItemDto ToDto(this TodoItem model)
    {
        return new TodoItemDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            Authors = model.Authors.Select(x => new AuthorIdDto { Id = x.Id }).ToList(),
            Workspace = new WorkspaceIdDto { Id = model.WorkspaceId },
            IsCompleted = model.IsCompleted,
        };
    }

    public static TodoItem ToModel(this TodoItemUpdateInput updateDto, TodoItemIdDto idDto)
    {
        var todoItem = new TodoItem { Id = idDto.Id, IsCompleted = updateDto.IsCompleted };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            todoItem.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            todoItem.UpdatedAt = updateDto.UpdatedAt;
        }

        return todoItem;
    }
}
