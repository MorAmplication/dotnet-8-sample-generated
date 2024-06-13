using Microsoft.AspNetCore.Mvc;

namespace Dotnet_8SampleApiDotNet.APIs;

[ApiController()]
public class AuthorsController : AuthorsControllerBase
{
    public AuthorsController(IAuthorsService service)
        : base(service) { }
}
