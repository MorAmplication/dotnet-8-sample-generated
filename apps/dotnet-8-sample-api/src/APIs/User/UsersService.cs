using Dotnet_8SampleApiDotNet.Infrastructure;

namespace Dotnet_8SampleApiDotNet.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(Dotnet_8SampleApiDotNetDbContext context)
        : base(context) { }
}
