using Basket.API.Entities;
using Carter;
using EShop.Shared.CQRS;
using MediatR;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName):IQuery<GetBasketResponse>;
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBaksetEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket", async (string userNAme, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userNAme));
                return Results.Ok(result);
            })
              .WithName("Get Basket")
              .WithDescription("Get Basket With User Name")
              .Produces<GetBasketResponse>()
              .ProducesProblem(StatusCodes.Status404NotFound)
              .WithSummary("Get Basket");

               
        }
    }
    public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBasketResponse>
    {
        public Task<GetBasketResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
