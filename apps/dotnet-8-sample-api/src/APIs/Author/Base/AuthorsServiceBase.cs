using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs;

public abstract class AuthorsServiceBase : IAuthorsService
{
    public AuthorsServiceBase(AuthorsServiceContext context) { }

    /// <summary>
    /// Connect multiple TodoItems records to Author
    /// </summary>
    public async Task ConnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var author = await _context
            .Authors.Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        var todoItems = await _context
            .TodoItems.Where(t => todoItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (todoItems.Count == 0)
        {
            throw new NotFoundException();
        }

        var newtodoItems = todoItems.Except(author.todoItems);
        author.todoItems.AddRange(newtodoItems);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple TodoItems records from Author
    /// </summary>
    public async Task DisconnectTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var author = await _context
            .Authors.Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);

        if (author == null)
        {
            throw new NotFoundException();
        }

        var todoItems = await _context
            .TodoItems.Where(t => todoItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var todoItem in todoItems)
        {
            author.TodoItems.Remove(todoItem);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple TodoItems records for Author
    /// </summary>
    public async Task<List<TodoItemDto>> FindTodoItems(
        AuthorIdDto idDto,
        TodoItemFindMany TodoItemFindMany
    )
    {
        var todoItems = await _context
            .TodoItems.Where(a => a.Authors.Any(todoItem => todoItem.Id == idDto.Id))
            .ApplyWhere(todoItemFindMany.Where)
            .ApplySkip(todoItemFindMany.Skip)
            .ApplyTake(todoItemFindMany.Take)
            .ApplyOrderBy(todoItemFindMany.SortBy)
            .ToListAsync();

        return todoItems.Select(x => x.ToDto());
    }

    /// <summary>
    /// Update multiple TodoItems records for Author
    /// </summary>
    public async Task UpdateTodoItems(AuthorIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var author = await _context
            .Authors.Include(t => t.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);

        if (author == null)
        {
            throw new NotFoundException();
        }

        var todoItems = await _context
            .TodoItems.Where(a => todoItemIdDtos.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (todoItems.Count == 0)
        {
            throw new NotFoundException();
        }

        author.TodoItems = todoItems;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Create one Author
    /// </summary>
    public async Task<AuthorDto> CreateAuthor(AuthorCreateInput inputDto)
    {
        var model = new Author { Name = createDto.Name, };
        if (createDto.Id != null)
        {
            model.Id = createDto.Id.Value;
        }

        if (createDto.TodoItemIds != null)
        {
            model.TodoItems = await _context
                .TodoItems.Where(todoItem =>
                    createDto.TodoItemIds.Select(t => t.Id).Contains(todoItem.Id)
                )
                .ToListAsync();
        }

        _context.Authors.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Author>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Author
    /// </summary>
    public async Task DeleteAuthor(AuthorIdDto inputDto)
    {
        var author = await _context.Authors.FindAsync(idDto.Id);
        if (author == null)
        {
            throw new NotFoundException();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Authors
    /// </summary>
    public async Task<List<AuthorDto>> Authors(AuthorFindMany findManyArgs)
    {
        var authors = await _context
            .Authors.Include(x => x.TodoItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return authors.ConvertAll(author => author.ToDto());
    }

    /// <summary>
    /// Get one Author
    /// </summary>
    public async Task<AuthorDto> Author(AuthorIdDto idDto)
    {
        var authors = await this.Authors(
            new AuthorFindMany { Where = new AuthorWhereInput { Id = idDto.Id } }
        );
        var author = authors.FirstOrDefault();
        if (author == null)
        {
            throw new NotFoundException();
        }

        return author;
    }

    /// <summary>
    /// Update one Author
    /// </summary>
    public async Task UpdateAuthor(AuthorUpdateInput updateDto)
    {
        var author = updateDto.ToModel(idDto);

        if (updateDto.TodoItemIds != null)
        {
            author.TodoItems = await _context
                .TodoItems.Where(todoItem =>
                    updateDto.TodoItemIds.Select(t => t.Id).Contains(todoItem.Id)
                )
                .ToListAsync();
        }

        _context.Entry(author).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(idDto))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
