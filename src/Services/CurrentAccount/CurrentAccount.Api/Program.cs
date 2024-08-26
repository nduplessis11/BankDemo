using Azure.Core;
using Azure.Identity;
using CurrentAccount.Api;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsLocal())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopmentOrLocal())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/CurrentAccount/Initiate", () => "Stub!")
    .WithName("CurrentAccountInitiate")
    .WithOpenApi();

// Test database connection:
app.MapGet("/test-connection", async (IConfiguration config, HttpContext context) =>
{
    var env = context.RequestServices.GetRequiredService<IHostEnvironment>();

    // Use system-assigned identity
    var sqlServerTokenProvider = new DefaultAzureCredential();

    AccessToken accessToken = await sqlServerTokenProvider.GetTokenAsync(
        new TokenRequestContext(scopes:
        [
            "https://ossrdbms-aad.database.windows.net/.default"
        ]));

    string connectionString = env.IsLocal()
        ? $"{config["AZURE_POSTGRESQL_CONNECTIONSTRING"]};Password={accessToken.Token}"
        : $"{Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CONNECTIONSTRING")};Password={accessToken.Token}";

    using var connection = new NpgsqlConnection(connectionString);
    await connection.OpenAsync();

    return Results.Ok("Database connection successful!");
})
.WithName("TestConnection")
.WithOpenApi();

app.Run();