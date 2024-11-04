using Carter;
using Catalog.API.Entities;
using Catalog.API.Products.CreateProduct;
using EShop.Shared.CQRS;
using Mapster;
using Marten;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery:IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductDto> Products);
    public record ProductDto(Guid Id,string Name,string Image,double Price,string Description);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products"
                , async(ISender sender) =>
                {
                    return await sender.Send(new GetProductsQuery());
                }).WithName("Get Products")
                 .Produces<GetProductsResult>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Get Product")
                .WithDescription("Get All Products");
                
        }
    }
    public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
           var products= await session.Query<Product>().ToListAsync(cancellationToken);

            return new GetProductsResult(products.Adapt<IEnumerable<ProductDto>>());
        }
    }
}
