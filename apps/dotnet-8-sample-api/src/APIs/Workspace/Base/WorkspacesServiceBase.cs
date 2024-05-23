using Dotnet_8SampleApiDotNet.APIs.Dtos;

namespace Dotnet_8SampleApiDotNet.APIs;

public abstract class WorkspacesServiceBase : IWorkspacesService
{
    public WorkspacesServiceBase(WorkspacesServiceContext context) { }

    /// <summary>
    /// Create one Workspace
    /// </summary>
    public async Task<WorkspaceDto> CreateWorkspace(WorkspaceCreateInput inputDto)
    {
        var model = new Workspace { Name = createDto.Name, };
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

        _context.Workspaces.Add(model);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Workspace>(model.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Workspace
    /// </summary>
    public async Task DeleteWorkspace(WorkspaceIdDto inputDto)
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
    public async Task UpdateWorkspace(WorkspaceUpdateInput updateDto)
    {
        var workspace = updateDto.ToModel(idDto);

        if (updateDto.TodoItemIds != null)
        {
            workspace.TodoItems = await _context
                .TodoItems.Where(todoItem =>
                    updateDto.TodoItemIds.Select(t => t.Id).Contains(todoItem.Id)
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
            if (!WorkspaceExists(idDto))
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

        var newtodoItems = todoItems.Except(workspace.todoItems);
        workspace.todoItems.AddRange(newtodoItems);
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
            workspace.TodoItems.Remove(todoItem);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple TodoItems records for Workspace
    /// </summary>
    public async Task<List<TodoItemDto>> FindTodoItems(
        WorkspaceIdDto idDto,
        TodoItemFindMany TodoItemFindMany
    )
    {
        var todoItems = await _context
            .TodoItems.Where(a => a.Workspaces.Any(todoItem => todoItem.Id == idDto.Id))
            .ApplyWhere(todoItemFindMany.Where)
            .ApplySkip(todoItemFindMany.Skip)
            .ApplyTake(todoItemFindMany.Take)
            .ApplyOrderBy(todoItemFindMany.SortBy)
            .ToListAsync();

        return todoItems.Select(x => x.ToDto());
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
            .TodoItems.Where(a => todoItemIdDtos.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (todoItems.Count == 0)
        {
            throw new NotFoundException();
        }

        workspace.TodoItems = todoItems;
        await _context.SaveChangesAsync();
    }
}
