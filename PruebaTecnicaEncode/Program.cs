using PruebaTecnicaEncode;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("http://*:7166");

builder.Configuration.AddEnvironmentVariables();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
