
using Carter;
using Catalog.API.Entities;
using EShop.Shared.CQRS;
using Mapster;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Categories
        ,string Description,string Image,double Price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductCommand command, ISender sender) =>
                {
                    var result = await sender.Send(command);
                    return Results.Created($"/products/{result.Id}", result);
                })
                 .WithName("CreateProduct")
                 .Produces<CreateProductResult>(StatusCodes.Status201Created)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Create Product")
                 .WithDescription("Create new Product");

               
        }
    }
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product=command.Adapt<Product>();   
           
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
