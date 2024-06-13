using Dotnet_8SampleApiDotNet.Infrastructure;

namespace Dotnet_8SampleApiDotNet.APIs;

public class WorkspacesService : WorkspacesServiceBase
{
    public WorkspacesService(Dotnet_8SampleApiDotNetDbContext context)
        : base(context) { }
}
