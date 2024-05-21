using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs;

public abstract class TodoItemsServiceBase : ITodoItemsService
{
    public TodoItemsServiceBase(TodoItemsServiceContext context) { }

    /// <summary>
    /// Create one TodoItem
    /// </summary>
    public async Task<TodoItemDto> createTodoItem(TodoItemCreateInput inputDto)
    {
        var model = new TodoItem { Name = createDto.Name, };
        if (createDto.Id != null)
        {
            model.Id = createDto.Id.Value;
        }

        if (createDto.WorkspaceIds != null)
        {
            model.Workspaces = await _context
                .Workspaces.Where(workspace =>
                    createDto.WorkspaceIds.Select(t => t.Id).Contains(workspace.Id)
                )
                .ToListAsync();
        }

        if (createDto.AuthorIds != null)
        {
            model.Authors = await _context
                .Authors.Where(author => createDto.AuthorIds.Select(t => t.Id).Contains(author.Id))
                .ToListAsync();
        }

        _context.TodoItems.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TodoItem>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one TodoItem
    /// </summary>
    public async Task deleteTodoItem(TodoItemIdDTO inputDto)
    {
        var todoItem = await _context.TodoItems.FindAsync(idDto.Id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        _context.TodoItems.Remove(todoItem);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many TodoItems
    /// </summary>
    public async Task<List<TodoItemDto>> todoItems(TodoItemFindMany findManyArgs)
    {
        var todoItems = await _context
            .TodoItems.Include(x => x.Workspaces)
            .Include(x => x.Authors)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();

        return todoItems.ConvertAll(todoItem => todoItem.ToDto());
    }

    /// <summary>
    /// Get one TodoItem
    /// </summary>
    public async Task<TodoItemDto> todoItem(TodoItemIdDTO idDto)
    {
        var todoItems = await this.TodoItems(
            new TodoItemFindMany { Where = new TodoItemWhereInput { Id = idDto.Id } }
        );
        var todoItem = todoItems.FirstOrDefault();
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        return todoItem;
    }

    /// <summary>
    /// Connect multiple Authors records to TodoItem
    /// </summary>
    public async Task connectAuthors(TodoItemIdDTO idDto, AuthorIdDTO AuthorId)
    {
        var todoItem = await _context
            .TodoItems.Include(x => x.Authors)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var authors = await _context
            .Authors.Where(t => authorsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (authors.Count == 0)
        {
            throw new NotFoundException();
        }

        var newauthors = authors.Except(todoItem.authors);
        todoItem.authors.AddRange(newauthors);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Authors records from TodoItem
    /// </summary>
    public async Task disconnectAuthors(TodoItemIdDTO idDto, AuthorIdDTO AuthorId)
    {
        var todoItem = await _context
            .TodoItems.Include(x => x.Authors)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);

        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var authors = await _context
            .Authors.Where(t => authorsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var author in authors)
        {
            todoItem.Authors.Remove(author);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Authors records for TodoItem
    /// </summary>
    public async Task<List<AuthorDto>> findAuthors(
        TodoItemIdDTO idDto,
        AuthorFindMany AuthorFindMany
    )
    {
        var todoItem = await _context.todoItems.FirstAsync(x => x.Id == idDto.Id);

        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        return todoItem.Authors.Select(author => author.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Authors records for TodoItem
    /// </summary>
    public async Task updateAuthors(TodoItemIdDTO idDto, AuthorIdDTO AuthorId)
    {
        var todoItem = await _context
            .TodoItems.Include(t => t.Authors)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);

        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var authors = await _context
            .Authors.Where(a => authorIdDtos.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (authors.Count == 0)
        {
            throw new NotFoundException();
        }

        todoItem.Authors = authors;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update one TodoItem
    /// </summary>
    public async Task updateTodoItem(TodoItemUpdateInput updateDto)
    {
        var todoItem = updateDto.ToModel(idDto);

        if (updateDto.WorkspaceIds != null)
        {
            todoItem.Workspaces = await _context
                .Workspaces.Where(workspace =>
                    updateDto.WorkspaceIds.Select(t => t.Id).Contains(workspace.Id)
                )
                .ToListAsync();
        }

        if (updateDto.AuthorIds != null)
        {
            todoItem.Authors = await _context
                .Authors.Where(author => updateDto.AuthorIds.Select(t => t.Id).Contains(author.Id))
                .ToListAsync();
        }

        _context.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoItemExists(idDto))
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
