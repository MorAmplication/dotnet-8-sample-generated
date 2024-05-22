using System.Reflection;
using Dotnet_8SampleApiDotNet;
using Dotnet_8SampleApiDotNet.APIs;
using Dotnet_8SampleApiDotNet.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

builder.Services.RegisterServices();

builder.Services.AddApiAuthentication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.UseOpenApiAuthentication();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(builder =>
{
    builder.AddPolicy(
        "MyCorsPolicy",
        policy =>
        {
            policy
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(["localhost", "https://studio.apollographql.com"])
                .AllowCredentials();
        }
    );
});
builder.services.AddDbContext<DbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await RolesManager.SyncRoles(services, app.Configuration);
}

app.UseApiAuthentication();
app.UseCors();
app.MapGraphQLEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseStaticFiles();

    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-ui/swagger.css");
    });

    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await SeedDevelopmentData.SeedDevUser(services, app.Configuration);
    }
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();
