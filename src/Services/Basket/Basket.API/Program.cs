using Basket.API.Data;
using Basket.API.Entities;
using Carter;
using Discount.Grpc;
using EShop.Shared.Behaviors;
using EShop.Shared.Exceptions.Handlers;
using FluentValidation;
using Marten;
using Microsoft.Extensions.Caching.Distributed;

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
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var basketRepository=provider.GetRequiredService<BasketRepository>();
//    return new CachedBasketRepository(basketRepository,provider.GetRequiredService<IDistributedCache>());
//});
builder.Services
    .AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
    {
        options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        return handler;
    });
    

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });

app.Run();
