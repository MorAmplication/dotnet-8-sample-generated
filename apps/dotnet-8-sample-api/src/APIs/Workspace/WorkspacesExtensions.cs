using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;

namespace Dotnet_8SampleApiDotNet.APIs.Extensions;

public class WorkspacesExtensions
{
    public static Workspace ToModel(this WorkspaceUpdateInput updateDto, WorkspaceIdDto idDto)
    {
        var workspace = new Workspace { Id = idDto.Id, Name = updateDto.Name, };
        return workspace;
    }

    public static WorkspaceDto ToDto(this Workspace model)
    {
        return new WorkspaceDto
        {
            id = model.id,
            createdAt = model.createdAt,
            updatedAt = model.updatedAt,
            todoItems = model.TodoItems.Select(x => new TodoItemIdDto { Id = x.Id }).ToList(),
            name = model.name,
        };
    }
}
