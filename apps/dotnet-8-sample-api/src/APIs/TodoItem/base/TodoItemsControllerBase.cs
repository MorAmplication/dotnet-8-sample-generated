namespace Dotnet_8SampleApiDotNet.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TodoItemsControllerBase : ControllerBase
{
    public TodoItemsControllerBase(ITodoItemsService service) { }
}
