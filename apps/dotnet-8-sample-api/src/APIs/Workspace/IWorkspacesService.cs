using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs;

public interface IWorkspacesService
{
    /// <summary>
    /// Create one Workspace
    /// </summary>
    public Task<WorkspaceDto> CreateWorkspace(WorkspaceCreateInput workspaceDto);

    /// <summary>
    /// Delete one Workspace
    /// </summary>
    public Task DeleteWorkspace(WorkspaceIdDto idDto);

    /// <summary>
    /// Find many Workspaces
    /// </summary>
    public Task<List<WorkspaceDto>> Workspaces(WorkspaceFindMany findManyArgs);

    /// <summary>
    /// Get one Workspace
    /// </summary>
    public Task<WorkspaceDto> Workspace(WorkspaceIdDto idDto);

    /// <summary>
    /// Update one Workspace
    /// </summary>
    public Task UpdateWorkspace(WorkspaceIdDto idDto, WorkspaceUpdateInput updateDto);

    /// <summary>
    /// Connect multiple TodoItems records to Workspace
    /// </summary>
    public Task ConnectTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId);

    /// <summary>
    /// Disconnect multiple TodoItems records from Workspace
    /// </summary>
    public Task DisconnectTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId);

    /// <summary>
    /// Find multiple TodoItems records for Workspace
    /// </summary>
    public Task<List<TodoItemDto>> FindTodoItems(
        WorkspaceIdDto idDto,
        TodoItemFindMany TodoItemFindMany
    );

    /// <summary>
    /// Update multiple TodoItems records for Workspace
    /// </summary>
    public Task UpdateTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId);
}
