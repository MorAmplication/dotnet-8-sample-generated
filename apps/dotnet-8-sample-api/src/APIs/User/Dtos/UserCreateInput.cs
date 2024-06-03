namespace Dotnet_8SampleApiDotNet.APIs.Dtos;

public class UserCreateInput
{
    public string? Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UpdatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Username { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; }

    public string Roles { get; set; }
}
