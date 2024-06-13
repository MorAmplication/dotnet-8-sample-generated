using Dotnet_8SampleApiDotNet.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_8SampleApiDotNet.Infrastructure;

public class Dotnet_8SampleApiDotNetDbContext : IdentityDbContext<IdentityUser>
{
    public Dotnet_8SampleApiDotNetDbContext(
        DbContextOptions<Dotnet_8SampleApiDotNetDbContext> options
    )
        : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<Workspace> Workspaces { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<User> Users { get; set; }
}
