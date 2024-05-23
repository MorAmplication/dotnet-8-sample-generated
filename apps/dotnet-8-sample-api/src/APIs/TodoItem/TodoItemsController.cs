namespace Dotnet_8SampleApiDotNet.APIs;

[ApiController()]
public class TodoItemsController : TodoItemsControllerBase
{
    public TodoItemsController(ITodoItemsService service)
        : base(service) { }
}
