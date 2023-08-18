using Ramboe.MinimalApis;

var builder = WebApplication.CreateBuilder(args);

var isDev = builder.Environment.IsDevelopment();

builder.Services.AddEndpoints<Program>(builder.Configuration, isDev);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseEndpoints<Program>();
app.Run();
