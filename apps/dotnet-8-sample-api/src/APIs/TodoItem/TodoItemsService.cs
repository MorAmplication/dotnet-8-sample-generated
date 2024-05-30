using Dotnet_8SampleApiDotNet.Infrastructure;

namespace Dotnet_8SampleApiDotNet.APIs;

public class TodoItemsService : TodoItemsServiceBase
{
    public TodoItemsService(Dotnet_8SampleApiDotNetDbContext context)
        : base(context) { }
}
