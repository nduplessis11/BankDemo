var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "CurrentAccount service is running!");

app.Run();
