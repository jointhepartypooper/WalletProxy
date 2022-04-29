using Carter;

var builder = WebApplication.CreateBuilder(args);
            
builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IRpcClient>(new RPCClient(new StaticRestClientFactory()));
builder.Services.AddCarter();

await using var app = builder.Build();

app.UseCors();

//config endpoints with Carter:
//app.MapGet("/", () => "Hello! This is .NET 6 Minimal API App Service").ExcludeFromDescription();
app.MapCarter();

//Run the application.
app.Run("http://127.0.0.1:9009");

