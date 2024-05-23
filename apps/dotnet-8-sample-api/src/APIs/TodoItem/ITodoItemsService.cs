using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public interface ITodoItemsService
{
    /// <summary>
    /// Create one TodoItem
    /// </summary>
    public Task<TodoItemDto> CreateTodoItem(TodoItemCreateInput todoitemDto) { }

    /// <summary>
    /// Delete one TodoItem
    /// </summary>
    public Task DeleteTodoItem(TodoItemIdDto idDto) { }

    /// <summary>
    /// Find many TodoItems
    /// </summary>
    public Task<List<TodoItemDto>> TodoItems(TodoItemFindMany findManyArgs) { }

    /// <summary>
    /// Get one TodoItem
    /// </summary>
    public Task TodoItem(TodoItemIdDto idDto) { }

    /// <summary>
    /// Connect multiple Authors records to TodoItem
    /// </summary>
    public Task connectAuthors(TodoItemIdDto idDto, TodoItemIdDto[] TodoItemsId) { }

    /// <summary>
    /// Disconnect multiple Authors records from TodoItem
    /// </summary>
    public Task disconnectAuthors(TodoItemIdDto idDto, TodoItemIdDto[] TodoItemsId) { }

    /// <summary>
    /// Find multiple Authors records for TodoItem
    /// </summary>
    public Task<List<AuthorDto>> findAuthors(TodoItemIdDto idDto, AuthorFindMany AuthorFindMany) { }

    /// <summary>
    /// Update multiple Authors records for TodoItem
    /// </summary>
    public Task updateAuthors(TodoItemIdDto idDto, TodoItemIdDto[] TodoItemsId) { }

    /// <summary>
    /// Update one TodoItem
    /// </summary>
    public Task UpdateTodoItem(TodoItemUpdateInput updateInput) { }
}
