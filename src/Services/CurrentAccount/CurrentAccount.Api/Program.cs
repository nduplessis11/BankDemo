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



// This example assumes you want to open the connection as part of an endpoint:
app.MapGet("/test-connection", async () =>
{
    // Use system-assigned identity by default
    var sqlServerTokenProvider = new DefaultAzureCredential();

    // Uncomment this section and comment the line above if using user-assigned identity
    /*
    var sqlServerTokenProvider = new DefaultAzureCredential(
        new DefaultAzureCredentialOptions
        {
            ManagedIdentityClientId = Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CLIENTID")
        });
    */

    // Acquire the access token
    AccessToken accessToken = await sqlServerTokenProvider.GetTokenAsync(
        new TokenRequestContext(scopes: new string[]
        {
            "https://ossrdbms-aad.database.windows.net/.default"
        }));

    // Combine the token with the connection string from the environment variables provided by Service Connector
    string connectionString =
        $"{Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CONNECTIONSTRING")};Password={accessToken.Token}";

    // Establish the connection
    using (var connection = new NpgsqlConnection(connectionString))
    {
        Console.WriteLine("Opening connection using access token...");
        await connection.OpenAsync();
        
        // Here you can perform database operations
        // Example: return a success message
        return Results.Ok("Database connection successful!");
    }
})
.WithName("TestConnection")
.WithOpenApi();


app.Run();
