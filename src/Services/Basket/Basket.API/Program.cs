using Carter;
using EShop.Shared.Behaviors;
using EShop.Shared.Exceptions.Handlers;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));

});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
