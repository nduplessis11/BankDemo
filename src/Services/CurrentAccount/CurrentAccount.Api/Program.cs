using Azure.Core;
using Azure.Identity;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/CurrentAccount/Initiate", () => "Stub!")
    .WithName("CurrentAccountInitiate")
    .WithOpenApi();

// Test database connection:
app.MapGet("/test-connection", async () =>
{
    // Use system-assigned identity
    var sqlServerTokenProvider = new DefaultAzureCredential();

    AccessToken accessToken = await sqlServerTokenProvider.GetTokenAsync(
        new TokenRequestContext(scopes: new string[]
        {
            "https://ossrdbms-aad.database.windows.net/.default"
        }));

    // Combine the token with the connection string from the environment variables provided by Service Connector
    string connectionString =
        $"{Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CONNECTIONSTRING")};Password={accessToken.Token}";

    using (var connection = new NpgsqlConnection(connectionString))
    {
        await connection.OpenAsync();

        return Results.Ok("Database connection successful!");
    }
})
.WithName("TestConnection")
.WithOpenApi();

app.Run();
