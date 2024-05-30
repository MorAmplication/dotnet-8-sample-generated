using Dotnet_8SampleApiDotNet.APIs;
using Dotnet_8SampleApiDotNet.APIs.Common;
using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.APIs.Errors;
using Dotnet_8SampleApiDotNet.APIs.Extensions;
using Dotnet_8SampleApiDotNet.Infrastructure;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_8SampleApiDotNet.APIs;

public abstract class TodoItemsServiceBase : ITodoItemsService
{
    protected readonly Dotnet_8SampleApiDotNetDbContext _context;

    public TodoItemsServiceBase(Dotnet_8SampleApiDotNetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one TodoItem
    /// </summary>
    public async Task<TodoItemDto> CreateTodoItem(TodoItemCreateInput createDto)
    {
        var todoItem = new TodoItem
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            IsCompleted = createDto.IsCompleted
        };

        if (createDto.Id != null)
        {
            todoItem.Id = createDto.Id;
        }
        if (createDto.Authors != null)
        {
            todoItem.Authors = await _context
                .Authors.Where(author => createDto.Authors.Select(t => t.Id).Contains(author.Id))
                .ToListAsync();
        }

        _context.TodoItems.Add(todoItem);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TodoItem>(todoItem.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one TodoItem
    /// </summary>
    public async Task DeleteTodoItem(TodoItemIdDto idDto)
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
    public async Task<List<TodoItemDto>> TodoItems(TodoItemFindMany findManyArgs)
    {
        var todoItems = await _context
            .TodoItems.Include(x => x.Workspace)
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
    public async Task<TodoItemDto> TodoItem(TodoItemIdDto idDto)
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
    public async Task ConnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorsId)
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

        var authorsToConnect = authors.Except(todoItem.Authors);

        foreach (var author in authorsToConnect)
        {
            todoItem.Authors.Add(author);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Authors records from TodoItem
    /// </summary>
    public async Task DisconnectAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorsId)
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
            todoItem.Authors?.Remove(author);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Authors records for TodoItem
    /// </summary>
    public async Task<List<AuthorDto>> FindAuthors(
        TodoItemIdDto idDto,
        AuthorFindMany todoItemFindMany
    )
    {
        var authors = await _context
            .Authors.Where(m => m.TodoItems.Any(x => x.Id == idDto.Id))
            .ApplyWhere(todoItemFindMany.Where)
            .ApplySkip(todoItemFindMany.Skip)
            .ApplyTake(todoItemFindMany.Take)
            .ApplyOrderBy(todoItemFindMany.SortBy)
            .ToListAsync();

        return authors.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Get a Workspace record for TodoItem
    /// </summary>
    public async Task<WorkspaceDto> GetWorkspace(TodoItemIdDto idDto)
    {
        var todoItem = await _context
            .TodoItems.Where(todoItem => todoItem.Id == idDto.Id)
            .Include(todoItem => todoItem.Workspace)
            .FirstOrDefaultAsync();
        if (todoItem == null)
        {
            throw new NotFoundException();
        }
        return todoItem.Workspace.ToDto();
    }

    /// <summary>
    /// Update multiple Authors records for TodoItem
    /// </summary>
    public async Task UpdateAuthors(TodoItemIdDto idDto, AuthorIdDto[] authorsId)
    {
        var todoItem = await _context
            .TodoItems.Include(t => t.Authors)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (todoItem == null)
        {
            throw new NotFoundException();
        }

        var authors = await _context
            .Authors.Where(a => authorsId.Select(x => x.Id).Contains(a.Id))
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
    public async Task UpdateTodoItem(TodoItemIdDto idDto, TodoItemUpdateInput updateDto)
    {
        var todoItem = updateDto.ToModel(idDto);

        if (updateDto.Authors != null)
        {
            todoItem.Authors = await _context
                .Authors.Where(author => updateDto.Authors.Select(t => t.Id).Contains(author.Id))
                .ToListAsync();
        }

        _context.Entry(todoItem).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.TodoItems.Any(e => e.Id == todoItem.Id))
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
