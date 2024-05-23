using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_8SampleApiDotNet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WorkspacesControllerBase : ControllerBase
{
    public WorkspacesControllerBase(IWorkspacesService service) { }

    /// <summary>
    /// Create one Workspace
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<WorkspaceDto>> CreateWorkspace(WorkspaceCreateInput input)
    {
        var workspace = await _service.CreateWorkspace(input);

        return CreatedAtAction(nameof(Workspace), new { id = workspace.Id }, workspace);
    }

    /// <summary>
    /// Delete one Workspace
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task DeleteWorkspace([FromRoute()] WorkspaceIdDto idDto)
    {
        try
        {
            await _service.DeleteWorkspace(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Workspaces
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<WorkspaceDto>>> Workspaces(
        [FromQuery()] WorkspaceFindMany filter
    )
    {
        return Ok(await _service.Workspaces(filter));
    }

    /// <summary>
    /// Get one Workspace
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<WorkspaceDto>> Workspace([FromRoute()] WorkspaceIdDto idDto)
    {
        try
        {
            return await _service.Workspace(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Workspace
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task UpdateWorkspace(
        [FromRoute()] WorkspaceIdDto idDto,
        [FromQuery()] WorkspaceUpdateInput WorkspaceUpdateDto
    )
    {
        try
        {
            await _service.UpdateWorkspace(idDto, workspaceUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Connect multiple TodoItems records to Workspace
    /// </summary>
    [HttpPost("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task ConnectTodoItems(
        [FromRoute()] WorkspaceIdDto idDto,
        [FromQuery()] TodoItemIdDto[] todoItemsId
    )
    {
        try
        {
            await _service.ConnectTodoItems(idDto, todoItemIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple TodoItems records from Workspace
    /// </summary>
    [HttpDelete("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task DisconnectTodoItems(
        [FromRoute()] WorkspaceIdDto idDto,
        [FromBody()] TodoItemIdDto[] todoItemsId
    )
    {
        try
        {
            await _service.DisconnectTodoItems(idDto, todoItemIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple TodoItems records for Workspace
    /// </summary>
    [HttpGet("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task<List<TodoItemDto>> FindTodoItems(
        [FromRoute()] WorkspaceIdDto idDto,
        [FromQuery()] TodoItemFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindTodoItems(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple TodoItems records for Workspace
    /// </summary>
    [HttpPatch("{Id}/todoItems")]
    [Authorize(Roles = "admin,user")]
    public async Task UpdateTodoItems(
        [FromRoute()] WorkspaceIdDto idDto,
        [FromBody()] TodoItemIdDto[] todoItemsId
    )
    {
        try
        {
            await _service.UpdateTodoItems(idDto, todoItemIds);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
