using Carter;
using Catalog.API.Entities;
using Catalog.API.Exceptions;
using EShop.Shared.CQRS;
using Mapster;
using Marten;
using MediatR;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id):IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(ProductDto Product);
    public abstract record ProductDto(Guid Id,string Name,string Description,string Image,double Price);
    public class  GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) => await sender.Send(new GetProductByIdQuery(id))).WithName("GetProductById")
            .Produces<GetProductByIdResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
    public class  GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session
                .LoadAsync<Product>(query.Id,cancellationToken)
                ?? throw new ProductNotFoundException(query.Id);

            var productDto =product.Adapt<ProductDto>();
            return new GetProductByIdResult(productDto);
        }
    }
}
