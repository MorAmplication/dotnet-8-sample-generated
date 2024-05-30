using Dotnet_8SampleApiDotNet.APIs;
using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_8SampleApiDotNet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TodoItemsControllerBase : ControllerBase
{
    protected readonly ITodoItemsService _service;

    public TodoItemsControllerBase(ITodoItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one TodoItem
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemCreateInput input)
    {
        var todoItem = await _service.CreateTodoItem(input);

        return CreatedAtAction(nameof(TodoItem), new { id = todoItem.Id }, todoItem);
    }

    /// <summary>
    /// Delete one TodoItem
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTodoItem([FromRoute()] TodoItemIdDto idDto)
    {
        try
        {
            await _service.DeleteTodoItem(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many TodoItems
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<TodoItemDto>>> TodoItems(
        [FromQuery()] TodoItemFindMany filter
    )
    {
        return Ok(await _service.TodoItems(filter));
    }

    /// <summary>
    /// Get one TodoItem
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<TodoItemDto>> TodoItem([FromRoute()] TodoItemIdDto idDto)
    {
        try
        {
            return await _service.TodoItem(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Authors records to TodoItem
    /// </summary>
    [HttpPost("{Id}/authors")]
    public async Task<ActionResult> ConnectAuthors(
        [FromRoute()] TodoItemIdDto idDto,
        [FromQuery()] AuthorIdDto[] authorsId
    )
    {
        try
        {
            await _service.ConnectAuthors(idDto, authorsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Authors records from TodoItem
    /// </summary>
    [HttpDelete("{Id}/authors")]
    public async Task<ActionResult> DisconnectAuthors(
        [FromRoute()] TodoItemIdDto idDto,
        [FromBody()] AuthorIdDto[] authorsId
    )
    {
        try
        {
            await _service.DisconnectAuthors(idDto, authorsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Authors records for TodoItem
    /// </summary>
    [HttpGet("{Id}/authors")]
    public async Task<ActionResult<List<AuthorDto>>> FindAuthors(
        [FromRoute()] TodoItemIdDto idDto,
        [FromQuery()] AuthorFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindAuthors(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Workspace record for TodoItem
    /// </summary>
    [HttpGet("{Id}/workspaces")]
    public async Task<ActionResult<List<WorkspaceDto>>> GetWorkspace(
        [FromRoute()] TodoItemIdDto idDto
    )
    {
        var workspace = await _service.GetWorkspace(idDto);
        return Ok(workspace);
    }

    /// <summary>
    /// Update multiple Authors records for TodoItem
    /// </summary>
    [HttpPatch("{Id}/authors")]
    public async Task<ActionResult> UpdateAuthors(
        [FromRoute()] TodoItemIdDto idDto,
        [FromBody()] AuthorIdDto[] authorsId
    )
    {
        try
        {
            await _service.UpdateAuthors(idDto, authorsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one TodoItem
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateTodoItem(
        [FromRoute()] TodoItemIdDto idDto,
        [FromQuery()] TodoItemUpdateInput todoItemUpdateDto
    )
    {
        try
        {
            await _service.UpdateTodoItem(idDto, todoItemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
