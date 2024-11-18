using Carter;
using EShop.Shared.Behaviors;
using EShop.Shared.Exceptions.Handlers;
using FluentValidation;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg => 
{
        cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    
}); 
builder.Services.AddMarten(opt => 
{
    opt.Connection(builder.Configuration.GetConnectionString("Default")!);
}).UseLightweightSessions();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Default")!);
var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options=>{});
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
    
app.Run();

