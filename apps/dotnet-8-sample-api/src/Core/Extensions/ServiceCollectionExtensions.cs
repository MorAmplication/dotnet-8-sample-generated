using Dotnet_8SampleApiDotNet.APIs;

namespace Dotnet_8SampleApiDotNet;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorsService, AuthorsService>();
        services.AddScoped<ITodoItemsService, TodoItemsService>();
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IWorkspacesService, WorkspacesService>();
    }
}
