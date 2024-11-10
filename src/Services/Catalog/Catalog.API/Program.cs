using Carter;
using EShop.Shared.Behaviors;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => 
{
        cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    
}); 
builder.Services.AddMarten(opt => 
{
    opt.Connection(builder.Configuration.GetConnectionString("Default")!);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
var app = builder.Build();
app.MapCarter();
app.Run();

