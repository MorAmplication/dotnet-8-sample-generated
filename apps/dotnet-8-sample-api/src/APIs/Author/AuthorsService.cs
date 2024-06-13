using Dotnet_8SampleApiDotNet.Infrastructure;

namespace Dotnet_8SampleApiDotNet.APIs;

public class AuthorsService : AuthorsServiceBase
{
    public AuthorsService(Dotnet_8SampleApiDotNetDbContext context)
        : base(context) { }
}
