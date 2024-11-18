using Carter;
using Catalog.API.Entities;
using Catalog.API.Products.CreateProduct;
using EShop.Shared.CQRS;
using Mapster;
using Marten;
using Marten.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNo=1,int? PageSize=10):IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<ProductDto> Products);
    public record ProductDto(Guid Id,string Name,string Image,double Price,string Description);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products"
                , async ([AsParameters] GetProductsQuery query, ISender sender) =>
                {
                    return await sender.Send(query);
                }).WithName("Get Products")
                 .Produces<GetProductsResult>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Get Product")
                .WithDescription("Get All Products");
                
        }
    }
    public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
           var products= await session.Query<Product>().ToPagedListAsync(query.PageNo ?? 1,query.PageSize ?? 10,cancellationToken);

            return new GetProductsResult(products.Adapt<IEnumerable<ProductDto>>());
        }
    }
}
