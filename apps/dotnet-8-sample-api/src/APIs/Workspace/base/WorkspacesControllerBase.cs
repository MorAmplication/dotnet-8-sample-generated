namespace Dotnet_8SampleApiDotNet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class WorkspacesControllerBase : ControllerBase
{
    public WorkspacesControllerBase(IWorkspacesService service) { }
}
