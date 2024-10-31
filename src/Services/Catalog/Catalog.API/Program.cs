using Carter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCarter();
builder.Services.AddMediatR(cfg=>
    cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly)
);
var app = builder.Build();
app.MapCarter();
app.Run();
