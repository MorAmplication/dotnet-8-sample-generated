using Microsoft.AspNetCore.Mvc;

namespace Dotnet_8SampleApiDotNet.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
