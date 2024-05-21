using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public interface IWorkspacesService
{
    /// <summary>
    /// Create one Workspace
    /// </summary>
    public Task<WorkspaceDto> CreateWorkspace(WorkspaceCreateInput workspaceDto) { }

    /// <summary>
    /// Delete one Workspace
    /// </summary>
    public Task DeleteWorkspace(WorkspaceIdDto idDto) { }

    /// <summary>
    /// Find many Workspaces
    /// </summary>
    public Task<List<WorkspaceDto>> Workspaces(WorkspaceFindMany findManyArgs) { }

    /// <summary>
    /// Get one Workspace
    /// </summary>
    public Task Workspace(WorkspaceIdDto idDto) { }

    /// <summary>
    /// Update one Workspace
    /// </summary>
    public Task UpdateWorkspace(WorkspaceUpdateInput updateInput) { }

    /// <summary>
    /// Connect multiple TodoItems records to Workspace
    /// </summary>
    public Task connectTodoItems(WorkspaceIdDto idDto, WorkspaceIdDto[] WorkspacesId) { }

    /// <summary>
    /// Disconnect multiple TodoItems records from Workspace
    /// </summary>
    public Task disconnectTodoItems(WorkspaceIdDto idDto, WorkspaceIdDto[] WorkspacesId) { }

    /// <summary>
    /// Find multiple TodoItems records for Workspace
    /// </summary>
    public Task<List<TodoItemDto>> findTodoItems(
        WorkspaceIdDto idDto,
        TodoItemFindMany TodoItemFindMany
    ) { }

    /// <summary>
    /// Update multiple TodoItems records for Workspace
    /// </summary>
    public Task updateTodoItems(WorkspaceIdDto idDto, WorkspaceIdDto[] WorkspacesId) { }
}
