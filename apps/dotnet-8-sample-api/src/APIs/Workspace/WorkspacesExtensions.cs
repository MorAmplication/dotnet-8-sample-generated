using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;

namespace Dotnet_8SampleApiDotNet.APIs.Extensions;

public static class WorkspacesExtensions
{
    public static WorkspaceDto ToDto(this Workspace model)
    {
        return new WorkspaceDto
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            TodoItems = model.TodoItems?.Select(x => new TodoItemIdDto { Id = x.Id }).ToList(),
            Name = model.Name,
        };
    }

    public static Workspace ToModel(this WorkspaceUpdateInput updateDto, WorkspaceIdDto idDto)
    {
        var workspace = new Workspace { Id = idDto.Id, Name = updateDto.Name };

        // map required fields
        if (updateDto.CreatedAt != null)
        {
            workspace.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            workspace.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return workspace;
    }
}
