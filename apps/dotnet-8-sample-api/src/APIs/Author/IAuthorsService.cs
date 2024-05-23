using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public interface IAuthorsService
{
    /// <summary>
    /// Connect multiple TodoItems records to Author
    /// </summary>
    public Task connectTodoItems(AuthorIdDto idDto, AuthorIdDto[] AuthorsId) { }

    /// <summary>
    /// Disconnect multiple TodoItems records from Author
    /// </summary>
    public Task disconnectTodoItems(AuthorIdDto idDto, AuthorIdDto[] AuthorsId) { }

    /// <summary>
    /// Find multiple TodoItems records for Author
    /// </summary>
    public Task<List<TodoItemDto>> findTodoItems(
        AuthorIdDto idDto,
        TodoItemFindMany TodoItemFindMany
    ) { }

    /// <summary>
    /// Update multiple TodoItems records for Author
    /// </summary>
    public Task updateTodoItems(AuthorIdDto idDto, AuthorIdDto[] AuthorsId) { }

    /// <summary>
    /// Create one Author
    /// </summary>
    public Task<AuthorDto> CreateAuthor(AuthorCreateInput authorDto) { }

    /// <summary>
    /// Delete one Author
    /// </summary>
    public Task DeleteAuthor(AuthorIdDto idDto) { }

    /// <summary>
    /// Find many Authors
    /// </summary>
    public Task<List<AuthorDto>> Authors(AuthorFindMany findManyArgs) { }

    /// <summary>
    /// Get one Author
    /// </summary>
    public Task Author(AuthorIdDto idDto) { }

    /// <summary>
    /// Update one Author
    /// </summary>
    public Task UpdateAuthor(AuthorUpdateInput updateInput) { }
}
