namespace Dotnet_8SampleApiDotNet.APIs;

public class TodoItemsService : TodoItemsServiceBase
{
    public TodoItemsService(TodoItemsServiceContext context)
        : base(context) { }
}
