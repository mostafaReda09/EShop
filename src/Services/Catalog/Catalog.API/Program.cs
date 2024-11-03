using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg=>
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly)
);
builder.Services.AddMarten(opt => 
{
    opt.Connection(builder.Configuration.GetConnectionString("Default")!);
}).UseLightweightSessions();
var app = builder.Build();
app.MapCarter();
app.Run();

