using Dotnet_8SampleApiDotNet.APIs;
using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_8SampleApiDotNet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AuthorsControllerBase : ControllerBase
{
    protected readonly IAuthorsService _service;

    public AuthorsControllerBase(IAuthorsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Connect multiple TodoItems records to Author
    /// </summary>
    [HttpPost("{Id}/todoItems")]
    public async Task<ActionResult> ConnectTodoItems(
        [FromRoute()] AuthorIdDto idDto,
        [FromQuery()] TodoItemIdDto[] todoItemsId
    )
    {
        try
        {
            await _service.ConnectTodoItems(idDto, todoItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple TodoItems records from Author
    /// </summary>
    [HttpDelete("{Id}/todoItems")]
    public async Task<ActionResult> DisconnectTodoItems(
        [FromRoute()] AuthorIdDto idDto,
        [FromBody()] TodoItemIdDto[] todoItemsId
    )
    {
        try
        {
            await _service.DisconnectTodoItems(idDto, todoItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple TodoItems records for Author
    /// </summary>
    [HttpGet("{Id}/todoItems")]
    public async Task<ActionResult<List<TodoItemDto>>> FindTodoItems(
        [FromRoute()] AuthorIdDto idDto,
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
    /// Update multiple TodoItems records for Author
    /// </summary>
    [HttpPatch("{Id}/todoItems")]
    public async Task<ActionResult> UpdateTodoItems(
        [FromRoute()] AuthorIdDto idDto,
        [FromBody()] TodoItemIdDto[] todoItemsId
    )
    {
        try
        {
            await _service.UpdateTodoItems(idDto, todoItemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Create one Author
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<AuthorDto>> CreateAuthor(AuthorCreateInput input)
    {
        var author = await _service.CreateAuthor(input);

        return CreatedAtAction(nameof(Author), new { id = author.Id }, author);
    }

    /// <summary>
    /// Delete one Author
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAuthor([FromRoute()] AuthorIdDto idDto)
    {
        try
        {
            await _service.DeleteAuthor(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Authors
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<AuthorDto>>> Authors([FromQuery()] AuthorFindMany filter)
    {
        return Ok(await _service.Authors(filter));
    }

    /// <summary>
    /// Get one Author
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<AuthorDto>> Author([FromRoute()] AuthorIdDto idDto)
    {
        try
        {
            return await _service.Author(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Author
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAuthor(
        [FromRoute()] AuthorIdDto idDto,
        [FromQuery()] AuthorUpdateInput authorUpdateDto
    )
    {
        try
        {
            await _service.UpdateAuthor(idDto, authorUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
