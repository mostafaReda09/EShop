﻿
using Carter;
using Catalog.API.Entities;
using EShop.Shared.CQRS;
using FluentValidation;
using Mapster;
using Marten;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Categories
        ,string Description,string Image,double Price):ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    public class CrateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CrateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("price must be greater than 0");
        }
    }
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
    public class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product=command.Adapt<Product>();   
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
        }
    }
}
