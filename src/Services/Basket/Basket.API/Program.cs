using Basket.API.Data;
using Basket.API.Entities;
using Carter;
using EShop.Shared.Behaviors;
using EShop.Shared.Exceptions.Handlers;
using FluentValidation;
using Marten;

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
builder.Services
    .AddMarten(opt=>
    {
        opt.Connection(builder.Configuration.GetConnectionString("Default")!);
        opt.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    }).UseLightweightSessions();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
