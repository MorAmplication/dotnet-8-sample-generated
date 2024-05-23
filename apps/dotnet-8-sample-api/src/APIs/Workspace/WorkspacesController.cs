namespace Dotnet_8SampleApiDotNet.APIs;

[ApiController()]
public class WorkspacesController : WorkspacesControllerBase
{
    public WorkspacesController(IWorkspacesService service)
        : base(service) { }
}
