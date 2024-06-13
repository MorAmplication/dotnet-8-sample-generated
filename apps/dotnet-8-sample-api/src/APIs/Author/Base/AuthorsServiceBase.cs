using Dotnet_8SampleApiDotNet.APIs;
using Dotnet_8SampleApiDotNet.APIs.Common;
using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.APIs.Errors;
using Dotnet_8SampleApiDotNet.APIs.Extensions;
using Dotnet_8SampleApiDotNet.Infrastructure;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_8SampleApiDotNet.APIs;

public abstract class AuthorsServiceBase : IAuthorsService
{
    protected readonly Dotnet_8SampleApiDotNetDbContext _context;

    public AuthorsServiceBase(Dotnet_8SampleApiDotNetDbContext context)
    {
        _context = context;
    }

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

        var todoItemsToConnect = todoItems.Except(author.TodoItems);

        foreach (var todoItem in todoItemsToConnect)
        {
            author.TodoItems.Add(todoItem);
        }

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
            author.TodoItems?.Remove(todoItem);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple TodoItems records for Author
    /// </summary>
    public async Task<List<TodoItemDto>> FindTodoItems(
        AuthorIdDto idDto,
        TodoItemFindMany authorFindMany
    )
    {
        var todoItems = await _context
            .TodoItems.Where(m => m.Authors.Any(x => x.Id == idDto.Id))
            .ApplyWhere(authorFindMany.Where)
            .ApplySkip(authorFindMany.Skip)
            .ApplyTake(authorFindMany.Take)
            .ApplyOrderBy(authorFindMany.SortBy)
            .ToListAsync();

        return todoItems.Select(x => x.ToDto()).ToList();
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
            .TodoItems.Where(a => todoItemsId.Select(x => x.Id).Contains(a.Id))
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
    public async Task<AuthorDto> CreateAuthor(AuthorCreateInput createDto)
    {
        var author = new Author
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name
        };

        if (createDto.Id != null)
        {
            author.Id = createDto.Id;
        }
        if (createDto.TodoItems != null)
        {
            author.TodoItems = await _context
                .TodoItems.Where(todoItem =>
                    createDto.TodoItems.Select(t => t.Id).Contains(todoItem.Id)
                )
                .ToListAsync();
        }

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Author>(author.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Author
    /// </summary>
    public async Task DeleteAuthor(AuthorIdDto idDto)
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
    public async Task UpdateAuthor(AuthorIdDto idDto, AuthorUpdateInput updateDto)
    {
        var author = updateDto.ToModel(idDto);

        if (updateDto.TodoItems != null)
        {
            author.TodoItems = await _context
                .TodoItems.Where(todoItem =>
                    updateDto.TodoItems.Select(t => t.Id).Contains(todoItem.Id)
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
            if (!_context.Authors.Any(e => e.Id == author.Id))
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
