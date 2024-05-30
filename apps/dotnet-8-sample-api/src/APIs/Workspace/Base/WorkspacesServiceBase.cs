using Dotnet_8SampleApiDotNet.APIs;
using Dotnet_8SampleApiDotNet.APIs.Common;
using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.APIs.Errors;
using Dotnet_8SampleApiDotNet.APIs.Extensions;
using Dotnet_8SampleApiDotNet.Infrastructure;
using Dotnet_8SampleApiDotNet.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_8SampleApiDotNet.APIs;

public abstract class WorkspacesServiceBase : IWorkspacesService
{
    protected readonly Dotnet_8SampleApiDotNetDbContext _context;

    public WorkspacesServiceBase(Dotnet_8SampleApiDotNetDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Workspace
    /// </summary>
    public async Task<WorkspaceDto> CreateWorkspace(WorkspaceCreateInput createDto)
    {
        var workspace = new Workspace
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt,
            Name = createDto.Name
        };

        if (createDto.Id != null)
        {
            workspace.Id = createDto.Id;
        }
        if (createDto.TodoItems != null)
        {
            workspace.TodoItems = await _context
                .TodoItems.Where(todoItem =>
                    createDto.TodoItems.Select(t => t.Id).Contains(todoItem.Id)
                )
                .ToListAsync();
        }

        _context.Workspaces.Add(workspace);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Workspace>(workspace.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Workspace
    /// </summary>
    public async Task DeleteWorkspace(WorkspaceIdDto idDto)
    {
        var workspace = await _context.Workspaces.FindAsync(idDto.Id);
        if (workspace == null)
        {
            throw new NotFoundException();
        }

        _context.Workspaces.Remove(workspace);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Workspaces
    /// </summary>
    public async Task<List<WorkspaceDto>> Workspaces(WorkspaceFindMany findManyArgs)
    {
        var workspaces = await _context
            .Workspaces.Include(x => x.TodoItems)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return workspaces.ConvertAll(workspace => workspace.ToDto());
    }

    /// <summary>
    /// Get one Workspace
    /// </summary>
    public async Task<WorkspaceDto> Workspace(WorkspaceIdDto idDto)
    {
        var workspaces = await this.Workspaces(
            new WorkspaceFindMany { Where = new WorkspaceWhereInput { Id = idDto.Id } }
        );
        var workspace = workspaces.FirstOrDefault();
        if (workspace == null)
        {
            throw new NotFoundException();
        }

        return workspace;
    }

    /// <summary>
    /// Update one Workspace
    /// </summary>
    public async Task UpdateWorkspace(WorkspaceIdDto idDto, WorkspaceUpdateInput updateDto)
    {
        var workspace = updateDto.ToModel(idDto);

        if (updateDto.TodoItems != null)
        {
            workspace.TodoItems = await _context
                .TodoItems.Where(todoItem =>
                    updateDto.TodoItems.Select(t => t.Id).Contains(todoItem.Id)
                )
                .ToListAsync();
        }

        _context.Entry(workspace).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Workspaces.Any(e => e.Id == workspace.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Connect multiple TodoItems records to Workspace
    /// </summary>
    public async Task ConnectTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var workspace = await _context
            .Workspaces.Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (workspace == null)
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

        var todoItemsToConnect = todoItems.Except(workspace.TodoItems);

        foreach (var todoItem in todoItemsToConnect)
        {
            workspace.TodoItems.Add(todoItem);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple TodoItems records from Workspace
    /// </summary>
    public async Task DisconnectTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var workspace = await _context
            .Workspaces.Include(x => x.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (workspace == null)
        {
            throw new NotFoundException();
        }

        var todoItems = await _context
            .TodoItems.Where(t => todoItemsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var todoItem in todoItems)
        {
            workspace.TodoItems?.Remove(todoItem);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple TodoItems records for Workspace
    /// </summary>
    public async Task<List<TodoItemDto>> FindTodoItems(
        WorkspaceIdDto idDto,
        TodoItemFindMany workspaceFindMany
    )
    {
        var todoItems = await _context
            .TodoItems.Where(m => m.WorkspaceId == idDto.Id)
            .ApplyWhere(workspaceFindMany.Where)
            .ApplySkip(workspaceFindMany.Skip)
            .ApplyTake(workspaceFindMany.Take)
            .ApplyOrderBy(workspaceFindMany.SortBy)
            .ToListAsync();

        return todoItems.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple TodoItems records for Workspace
    /// </summary>
    public async Task UpdateTodoItems(WorkspaceIdDto idDto, TodoItemIdDto[] todoItemsId)
    {
        var workspace = await _context
            .Workspaces.Include(t => t.TodoItems)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (workspace == null)
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

        workspace.TodoItems = todoItems;
        await _context.SaveChangesAsync();
    }
}
