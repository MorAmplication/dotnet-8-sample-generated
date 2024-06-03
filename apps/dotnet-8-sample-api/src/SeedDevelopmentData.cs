using Dotnet_8SampleApiDotNet.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Dotnet_8SampleApiDotNet;

public class SeedDevelopmentData
{
    public static async Task SeedDevUser(
        IServiceProvider serviceProvider,
        IConfiguration configuration
    )
    {
        var context = serviceProvider.GetRequiredService<Dotnet_8SampleApiDotNetDbContext>();
        var amplicationRoles = configuration
            .GetSection("AmplicationRoles")
            .AsEnumerable()
            .Where(x => x.Value != null)
            .Select(x => x.Value.ToString())
            .ToArray();

        var user = new User
        {
            Id = model.Id,
            CreatedAt = model.CreatedAt,
            UpdatedAt = model.UpdatedAt,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Username = model.Username,
            Email = model.Email,
            Password = model.Password,
            Roles = model.Roles,
        };

        if (!context.Users.Any(u => u.UserName == user.UserName))
        {
            var password = new PasswordHasher<User>();
            var hashed = password.HashPassword(user, "password");
            user.PasswordHash = hashed;
            var userStore = new UserStore<User>(context);
            await userStore.CreateAsync(user);
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach (var role in amplicationRoles)
            {
                await userStore.AddToRoleAsync(user, _roleManager.NormalizeKey(role));
            }
        }

        await context.SaveChangesAsync();
    }
}
