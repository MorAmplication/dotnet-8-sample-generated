using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs;

public interface IAuthorsService
{
    /// <summary>
    /// Connect multiple TodoItems records to Author
    /// </summary>
    public Task ConnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId);

    /// <summary>
    /// Disconnect multiple TodoItems records from Author
    /// </summary>
    public Task DisconnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId);

    /// <summary>
    /// Find multiple TodoItems records for Author
    /// </summary>
    public Task<List<TodoItemDto>> FindTodoItems(
        AuthorIdDto idDto,
        TodoItemFindMany TodoItemFindMany
    );

    /// <summary>
    /// Update multiple TodoItems records for Author
    /// </summary>
    public Task UpdateTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId);

    /// <summary>
    /// Create one Author
    /// </summary>
    public Task<AuthorDto> CreateAuthor(AuthorCreateInput authorDto);

    /// <summary>
    /// Delete one Author
    /// </summary>
    public Task DeleteAuthor(AuthorIdDto idDto);

    /// <summary>
    /// Find many Authors
    /// </summary>
    public Task<List<AuthorDto>> Authors(AuthorFindMany findManyArgs);

    /// <summary>
    /// Get one Author
    /// </summary>
    public Task<AuthorDto> Author(AuthorIdDto idDto);

    /// <summary>
    /// Update one Author
    /// </summary>
    public Task UpdateAuthor(AuthorIdDto idDto, AuthorUpdateInput updateDto);
}
