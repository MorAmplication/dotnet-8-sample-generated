namespace Dotnet_8SampleApiDotNet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AuthorsControllerBase : ControllerBase
{
    public AuthorsControllerBase(IAuthorsService service) { }
}
