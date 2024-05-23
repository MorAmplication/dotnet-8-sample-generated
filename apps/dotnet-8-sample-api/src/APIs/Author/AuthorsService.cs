namespace Dotnet_8SampleApiDotNet.APIs;

public class AuthorsService : AuthorsServiceBase
{
    public AuthorsService(AuthorsServiceContext context)
        : base(context) { }
}
