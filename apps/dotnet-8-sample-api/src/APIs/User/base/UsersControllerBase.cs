using Dotnet_8SampleApiDotNet.APIs;
using Dotnet_8SampleApiDotNet.APIs.Dtos;
using Dotnet_8SampleApiDotNet.APIs.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_8SampleApiDotNet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class UsersControllerBase : ControllerBase
{
    protected readonly IUsersService _service;

    public UsersControllerBase(IUsersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    [HttpPost()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<UserDto>> CreateUser(UserCreateInput input)
    {
        var user = await _service.CreateUser(input);

        return CreatedAtAction(nameof(User), new { id = user.Id }, user);
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    [HttpDelete("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> DeleteUser([FromRoute()] UserIdDto idDto)
    {
        try
        {
            await _service.DeleteUser(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    [HttpGet()]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<List<UserDto>>> Users([FromQuery()] UserFindMany filter)
    {
        return Ok(await _service.Users(filter));
    }

    /// <summary>
    /// Get one User
    /// </summary>
    [HttpGet("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult<UserDto>> User([FromRoute()] UserIdDto idDto)
    {
        try
        {
            return await _service.User(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one User
    /// </summary>
    [HttpPatch("{Id}")]
    [Authorize(Roles = "admin,user")]
    public async Task<ActionResult> UpdateUser(
        [FromRoute()] UserIdDto idDto,
        [FromQuery()] UserUpdateInput userUpdateDto
    )
    {
        try
        {
            await _service.UpdateUser(idDto, userUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
