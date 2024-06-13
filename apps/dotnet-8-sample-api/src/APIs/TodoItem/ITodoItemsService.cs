using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs;

public interface ITodoItemsService
{
    /// <summary>
    /// Create one TodoItem
    /// </summary>
    public Task<TodoItemDto> CreateTodoItem(TodoItemCreateInput todoitemDto);

    /// <summary>
    /// Delete one TodoItem
    /// </summary>
    public Task DeleteTodoItem(TodoItemIdDto idDto);

    /// <summary>
    /// Find many TodoItems
    /// </summary>
    public Task<List<TodoItemDto>> TodoItems(TodoItemFindMany findManyArgs);

    /// <summary>
    /// Get one TodoItem
    /// </summary>
    public Task<TodoItemDto> TodoItem(TodoItemIdDto idDto);

    /// <summary>
    /// Connect multiple Authors records to TodoItem
    /// </summary>
    public Task ConnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorsId);

    /// <summary>
    /// Disconnect multiple Authors records from TodoItem
    /// </summary>
    public Task DisconnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorsId);

    /// <summary>
    /// Find multiple Authors records for TodoItem
    /// </summary>
    public Task<List<AuthorDto>> FindAuthors(TodoItemIdDto idDto, AuthorFindMany AuthorFindMany);

    /// <summary>
    /// Get a Workspace record for TodoItem
    /// </summary>
    public Task<WorkspaceDto> GetWorkspace(TodoItemIdDto idDto);

    /// <summary>
    /// Update multiple Authors records for TodoItem
    /// </summary>
    public Task UpdateAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorsId);

    /// <summary>
    /// Update one TodoItem
    /// </summary>
    public Task UpdateTodoItem(TodoItemIdDto idDto, TodoItemUpdateInput updateDto);
}
